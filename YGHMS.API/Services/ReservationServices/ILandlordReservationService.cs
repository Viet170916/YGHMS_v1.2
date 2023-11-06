// using AutoMapper;
// using YghRentalManagementSystem.Common;
// using YghRentalManagementSystem.DTO.ResponseModels.ReservationDTOs;
// using YghRentalManagementSystem.Helpers;
// using YghRentalManagementSystem.Infra;
// using YghRentalManagementSystem.Services.ServicesExtensions;
//
// namespace YghRentalManagementSystem.Services;
//
// public interface ILandlordReservationService
// {
//   public IList<LandlordReservationDto> GetPagedReservation(int userId, int page, int? postId, int? status);
//   public ReservationDetailRes GetReservationById(int userId, int id);
//   public void SetStatusToReservation(int reservationId, int userId, int? state);
//   public void DeleteReservation(int reservationId, int userId);
//
//   // public ResponseDTO<List<LandlordReservationDto>> GetPagedReservation();
//   // public ResponseDTO<List<LandlordReservationDto>> GetPagedReservation();
// }
//
// public class LandlordReservationService : ILandlordReservationService
// {
//   private readonly RentalManagementContext _context;
//   public readonly IMapper _mapper;
//
//   public LandlordReservationService(RentalManagementContext context, IMapper mapper)
//   {
//     _context = context;
//     _mapper = mapper;
//   }
//
//   public IList<LandlordReservationDto> GetPagedReservation(int userId, int page, int? postId, int? status)
//   {
//     var landlordReservationsAll = _context.Reservations
//                                           .RealExist()
//                                           .Private(userId);
//                                           
//     if (postId != null)
//       landlordReservationsAll = landlordReservationsAll
//         .Where(reservation => reservation.Apartment.AccommodationId == postId);
//     if (status != null)
//       landlordReservationsAll = landlordReservationsAll
//         .Where(reservation => reservation.Status == status);
//     return landlordReservationsAll
//            .OrderByDescending(reservation => reservation.CreateAt)
//            .Paging(page, 10)
//            .SelectLandlordReservationDto()
//            .ToList();
//   }
//
//   public ReservationDetailRes GetReservationById(int userId, int id)
//   {
//     return _context.Reservations
//                    .RealExist()
//                    .Private(userId)
//                    .Where(reservation => reservation.Id == id && reservation.OwnerId == userId)
//                    .SelectReservationDetailRes()
//                    .First();
//   }
//
//   public void SetStatusToReservation(int reservationId, int userId, int? state)
//   {
//     var update = _context.Reservations
//                          .RealExist()
//                          .Private(userId)
//
//                          .First(reservation => reservation.Id == reservationId)
//                          .Status = state ?? 0;
//     _context.SaveChanges();
//   }
//
//   public void DeleteReservation(int reservationId, int userId)
//   {
//     var update = _context.Reservations
//                          .RealExist()
//                          .Private(userId)
//       .First(reservation => reservation.Id == reservationId)
//                          .IsDeleted = true;
//     _context.SaveChanges();
//   }
// }
