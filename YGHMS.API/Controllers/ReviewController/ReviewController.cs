using Microsoft.AspNetCore.Mvc;
using YGHMS.API.DTO.Common;
using YGHMS.API.DTO.ReviewDTOs;
using YGHMS.API.Services.ReviewServices;

namespace YGHMS.API.Controllers.ReviewController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : BaseApiController
    {
        private readonly ILogger<ReviewController> _logger;
        public readonly IReviewService _reviewService;
        public ReviewController(ILogger<ReviewController> logger, IReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
        }
        [HttpPost("create-review")]
        public ResponseDTO<ReviewResponseDTO> CreateReview(ReviewRequestDTO request)
        {
            if (UserHeader == null) throw new UnauthorizedAccessException();
            request.UserId = UserHeader.UserId;
            return _reviewService.CreateReview(request);
        }
        [HttpPost("udpate-review")]
        public ResponseDTO<ReviewResponseDTO> UpdateReview(ReviewRequestDTO request)
        {
            if (UserHeader == null) throw new UnauthorizedAccessException();
            request.UserId = UserHeader.UserId;
            return _reviewService.UpdateReview(request);
        }

        [HttpPost("delete-review")]
        public ResponseDTO<bool> DeleteReview(int reviewId)
        {
            if (UserHeader == null) throw new UnauthorizedAccessException();
            return _reviewService.DeleteReview(UserHeader.UserId, reviewId);
        }
        [HttpGet("get-list-review-post")]
        public ResponseDTO<List<ReviewResponseDTO>> GetListReview(int postId)
        {
            return _reviewService.GetListReviewByPostId(postId);
        }
        [HttpGet("get-list-review-apartment")]
        public ResponseDTO<List<ReviewResponseDTO>> GetListReviewByApartmentId(int apartmentId)
        {
            return _reviewService.GetListReviewByApartmentId(apartmentId);
        }
    }
}
