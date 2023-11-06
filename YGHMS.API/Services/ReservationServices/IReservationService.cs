using AutoMapper;
using YGHMS.API.Common;
using YGHMS.API.DTO.RequestModels;
using YGHMS.API.DTO.ResponseModels.ReservationDTOs;
using YGHMS.API.Helpers;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Infra.Models;

namespace YGHMS.API.Services.ReservationServices;

public interface IReservationService
{
  public IList<LandlordReservationDto> GetPagedReservation(int userId, int page, int? postId, int? status);
  public ReservationDetailRes? GetReservationById(int userId, int id);
  public void SetStatusToReservation(int reservationId, int userId, int? state);
  public void DeleteReservation(int reservationId, int userId);
  public TenantReservationDto? UpdateReservation(int? reservationId, BookingRequest reservation, int userId);
  public TenantReservationDto? CreateAReservation(BookingRequest reservation, int userId);
  public IList<TenantReservationDto> GetTenantPagedReservation(int userId, int page, int? postId, int? status);
  public TenantReservationDto? GetReservation(int? reservationId, int status, int userId);
}

public class ReservationService : IReservationService
{
  private readonly RentalManagementContext _context;
  public readonly IMapper _mapper;

  public ReservationService(RentalManagementContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public IList<LandlordReservationDto> GetPagedReservation(int userId, int page, int? postId, int? status)
  {
    var landlordReservationsAll = _context.Reservations
                                          .Where(re => re.Status != (int)ReservationStatus.DRAFT)
                                          .RealExist()
                                          .Private(userId);
    if (postId != null)
      landlordReservationsAll = landlordReservationsAll
        .Where(reservation => reservation.Apartment.AccommodationId == postId);
    if (status != null)
      landlordReservationsAll = landlordReservationsAll
        .Where(reservation => reservation.Status == status);
    return landlordReservationsAll
           .OrderByDescending(reservation => reservation.ModifyAt)
           .Paging(page, 10)
           .SelectLandlordReservationDto()
           .ToList();
  }

  public ReservationDetailRes? GetReservationById(int userId, int id)
  {
    return _context.Reservations
                   .Where(re => re.Status != (int)ReservationStatus.DRAFT)
                   .RealExist()
                   .Private(userId)
                   .Where(reservation => reservation.Id == id && reservation.OwnerId == userId)
                   .SelectReservationDetailRes()
                   .FirstOrDefault();
  }

  public void SetStatusToReservation(int reservationId, int userId, int? state)
  {
    _context.Reservations
            .Where(re => re.Status != (int)ReservationStatus.DRAFT)
            .Private(userId)
            .First(reservation => reservation.Id == reservationId)
            .Status = state ?? 0;
    _context.SaveChanges();
  }

  public void DeleteReservation(int reservationId, int userId)
  {
    _context.Reservations
            .RealExist()
            .Private(userId)
            .First(reservation => reservation.Id == reservationId)
            .IsDeleted = true;
    _context.SaveChanges();
  }

  public TenantReservationDto? CreateAReservation(BookingRequest reservation, int userId)
  {
    if (reservation.FromDate is null || reservation.ToDate is null) throw new(Constants.ErrorMessages.RangeDateIsNull);
    var user = _context.Users.FirstOrDefault(user => user.Id == userId);
    if (user is null) throw new(Constants.ErrorMessages.NoRight);
    var ownerId = _context.Users
                          .Select(ur => new { ur.Id, ur.Accommodations, })
                          .FirstOrDefault(us => us.Accommodations
                                                  .Any(acc => acc.Apartments
                                                                 .Any(ap => ap.Id == reservation.ApartmentId)))!
                          .Id;
    _context.Reservations.Add(new Reservation()
    {
      IsDeleted = false,
      CreateAt = reservation.CreateAt ?? DateTime.Now,
      ModifyAt = reservation.CreateAt ?? DateTime.Now,
      UserId = userId,
      Status = (int)ReservationStatus.DRAFT,
      //...
      OwnerId = ownerId,
      FromDate = (DateTime)reservation.FromDate!,
      ToDate = (DateTime)reservation.ToDate!,
      ApartmentId = (int)reservation.ApartmentId!,
      BedType = (int)reservation.BedType!,
    });
    _context.SaveChanges();
    return _context.Reservations
                   .Where(re => re.UserId == userId)
                   .Where(re => !re.IsDeleted && re.Status == (int)ReservationStatus.DRAFT)
                   .OrderByDescending(re => re.ModifyAt)
                   .SelectTenantReservation()
                   .FirstOrDefault()!;
  }

  public IList<TenantReservationDto> GetTenantPagedReservation(int userId, int page, int? postId, int? status)
  {
    var reservationsAll = _context.Reservations
                                  .RealExist()
                                  .TenantPrivate(userId);
    if (postId != null)
      reservationsAll = reservationsAll
        .Where(reservation => reservation.Apartment.AccommodationId == postId);
    if (status != null)
      reservationsAll = reservationsAll
        .Where(reservation => reservation.Status == status);
    return reservationsAll
           .OrderByDescending(reservation => reservation.ModifyAt)
           .Paging(page, 10)
           .SelectTenantReservation()
           .ToList();
  }

  public TenantReservationDto? UpdateReservation(int? reservationId, BookingRequest? reservation, int userId)
  {
    var user = _context.Users.FirstOrDefault(user => user.Id == userId);
    var existingReservation = _context.Reservations
                                      .FirstOrDefault(re =>
                                        re.Id == reservationId && re.Status == (int)ReservationStatus.DRAFT);
    if (string.IsNullOrEmpty(reservation!.User!.FullName))
    {
      throw new(Constants.ErrorMessages.BAD_PARAM_USER_FULLNAME);
    }

    if (string.IsNullOrEmpty(reservation.User.PhoneNumber))
    {
      throw new(Constants.ErrorMessages.BAD_PARAM_USER_PHONENUMBER);
    }

    if (user != null && existingReservation != null)
    {
      if ((user.FullName != reservation.User.FullName || user.PhoneNumber != reservation.User.PhoneNumber))
      {
        user.FullName = reservation.User.FullName;
        user.PhoneNumber = reservation.User.PhoneNumber;
        user.ModifyAt = reservation.CreateAt ?? DateTime.Now;
      }

      existingReservation.ModifyAt = reservation.CreateAt ?? DateTime.Now;
      existingReservation.Status = (int)ReservationStatus.PENDING;
      existingReservation.FromDate = (DateTime)reservation.FromDate!;
      existingReservation.ToDate = (DateTime)reservation.ToDate!;
      existingReservation.BedType = (int)reservation.BedType!;
      _context.SaveChanges();
      return _context.Reservations
                     .Where(re => re.UserId == userId)
                     .Where(re => !re.IsDeleted && re.Status == (int)ReservationStatus.PENDING)
                     .OrderByDescending(re => re.ModifyAt)
                     .SelectTenantReservation()
                     .FirstOrDefault()!;
    }

    throw new("Not exist");
  }

  public TenantReservationDto? GetReservation(int? reservationId, int status, int userId)
  {
    return _context.Reservations
                   .Where(re => re.UserId == userId && re.Id == reservationId)
                   .Where(re => !re.IsDeleted && re.Status == status)
                   .OrderByDescending(re => re.ModifyAt)
                   .SelectTenantReservation()
                   .FirstOrDefault()!;
  }
}