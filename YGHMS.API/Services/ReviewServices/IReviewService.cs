using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YGHMS.API.DTO.Common;
using YGHMS.API.DTO.ReviewDTOs;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Const;
using YGHMS.API.Infra.Models;

namespace YGHMS.API.Services.ReviewServices
{
    public interface IReviewService
    {
        public ResponseDTO<ReviewResponseDTO> CreateReview(ReviewRequestDTO request);
        public ResponseDTO<ReviewResponseDTO> UpdateReview(ReviewRequestDTO request);
        public ResponseDTO<bool> DeleteReview(int userId,int reivewId);
        public ResponseDTO<List<ReviewResponseDTO>> GetListReviewByPostId(int postId);
        public ResponseDTO<List<ReviewResponseDTO>> GetListReviewByApartmentId(int apartmentId);
    }
    public class ReviewService : IReviewService
    {
        private readonly ILogger<ReviewService> _logger;
        private readonly IMapper _mapper;
        private readonly RentalManagementContext _context;
        public ReviewService(ILogger<ReviewService> logger, IMapper mapper, RentalManagementContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }
        public ResponseDTO<ReviewResponseDTO> CreateReview(ReviewRequestDTO request)
        {
            var review = _mapper.Map<Review>(request);
            if(_context.Reviews.FirstOrDefault(e => e.UserId == request.UserId && e.ReservationId == request.ReservationId) != null)
            {
                return new ResponseDTO<ReviewResponseDTO>
                {
                    Code = (int)RESPONSE_CODE.BadRequest,
                    Message = ResponseMessage.ALREADY_REVIEWED
                };
            }
            if(request.Rate <= 0 || request.Rate > 5)
            {
                return new ResponseDTO<ReviewResponseDTO>
                {
                    Code = (int)RESPONSE_CODE.BadRequest
                };
            }
            var reservation = _context.Reservations.FirstOrDefault(e => e.Id == request.ReservationId && !e.IsDeleted);
            if(reservation == null)
            {
                return new ResponseDTO<ReviewResponseDTO>
                {
                    Code = (int)RESPONSE_CODE.BadRequest,
                };
            }
            if(reservation.UserId != request.UserId)
            {
                return new ResponseDTO<ReviewResponseDTO>
                {
                    Code = (int)RESPONSE_CODE.Unauthorized,
                };
            }
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return new ResponseDTO<ReviewResponseDTO>
            {
                Data = _mapper.Map<ReviewResponseDTO>(review)
            };
        }

        public ResponseDTO<bool> DeleteReview(int userId,int reivewId)
        {
            var review = _context.Reviews.Include(e => e.Reservation)
                                         .FirstOrDefault(e => e.Id == reivewId && !e.IsDeleted);
            if(review == null)
            {
                return new ResponseDTO<bool>
                {
                    Code = (int)RESPONSE_CODE.NotFound,
                    Message = ResponseMessage.REVIEW_DOES_NOT_EXIST
                };
            }
            if(review.Reservation.OwnerId != userId && review.UserId != userId)
            {
                return new ResponseDTO<bool>
                {
                    Code = (int)RESPONSE_CODE.Unauthorized,
                    Message = ResponseMessage.UNAUTHORIZED,
                    Data = false
                };
            }
            review.IsDeleted = true;
            _context.SaveChanges();
            return new ResponseDTO<bool>(true);
        }

        public ResponseDTO<List<ReviewResponseDTO>> GetListReviewByApartmentId(int apartmentId)
        {
            var apartment = _context.Apartments.Include(e => e.Reservations)
                                               .ThenInclude(e => e.Reviews)
                                               .FirstOrDefault(e => e.Id == apartmentId && !e.IsDeleted);
            if(apartment == null)
            {
                return new ResponseDTO<List<ReviewResponseDTO>>
                {
                    Code = (int)RESPONSE_CODE.NotFound,
                    Data = new List<ReviewResponseDTO>()
                };
            }
            var listReview = new List<ReviewResponseDTO>();
            var listReservation = apartment.Reservations.ToList();
            listReservation.ForEach(reservation =>
            {
                var reviews = reservation.Reviews.ToList();
                reviews.ForEach(review =>
                {
                    listReview.Add(_mapper.Map<ReviewResponseDTO>(review));
                });
            });
            return new ResponseDTO<List<ReviewResponseDTO>>(listReview);
        }

        public ResponseDTO<List<ReviewResponseDTO>> GetListReviewByPostId(int postId)
        {
            var apartmentIds = _context.Apartments.Where(e => e.Id == postId && !e.IsDeleted)
                                                  .Select(e => e.Id)
                                                  .ToList();
            if(apartmentIds == null || apartmentIds.Count == 0)
            {
                return new ResponseDTO<List<ReviewResponseDTO>>
                {
                    Code = (int)RESPONSE_CODE.NotFound,
                    Data = new List<ReviewResponseDTO>()
                };
            }
            var listReview = new List<ReviewResponseDTO>();
            apartmentIds.ForEach(apartment =>
            {
                var reviews = GetListReviewByApartmentId(apartment).Data;
                listReview.AddRange(reviews);
            });
            return new ResponseDTO<List<ReviewResponseDTO>>
            {
                Code = 200,
                Data = listReview
            };
        }

        public ResponseDTO<ReviewResponseDTO> UpdateReview(ReviewRequestDTO request)
        {
            var review = _context.Reviews.FirstOrDefault(e => e.Id == request.ReservationId && !e.IsDeleted);
            if(review == null)
            {
                return new ResponseDTO<ReviewResponseDTO>
                {
                    Code = (int)RESPONSE_CODE.NotFound,
                    Message = ResponseMessage.REVIEW_DOES_NOT_EXIST
                };
            }
            if(review.UserId != request.UserId)
            {
                return new ResponseDTO<ReviewResponseDTO>
                {
                    Code = (int)RESPONSE_CODE.Unauthorized,
                    Message = ResponseMessage.UNAUTHORIZED
                };
            }
            review.Comment = request.Comment;
            review.Rate = request.Rate;
            _context.SaveChanges();
            return new ResponseDTO<ReviewResponseDTO>
            {
                Code = 200,
                Data = _mapper.Map<ReviewResponseDTO>(review)
            };
        }
    }
}
