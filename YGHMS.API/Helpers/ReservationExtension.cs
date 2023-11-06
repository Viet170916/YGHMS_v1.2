using YGHMS.API.DTO.ResponseModels.ApartmentDTOs;
using YGHMS.API.DTO.ResponseModels.ReservationDTOs;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Infra.Models;
using Uri = YGHMS.API.Common.Uri;

namespace YGHMS.API.Helpers;

public static class ReservationExtension
{
  public static IQueryable<Reservation> Private(
    this IQueryable<Reservation> reservations,
    int userId,
    bool deleted = false
  )
  {
    return reservations.Where(reservation => reservation.IsDeleted == deleted && reservation.OwnerId == userId);
  }
  public static IQueryable<Reservation> TenantPrivate(
    this IQueryable<Reservation> reservations,
    int userId,
    bool deleted = false
  )
  {
    return reservations.Where(reservation => reservation.IsDeleted == deleted && reservation.UserId == userId);
  }

  public static IQueryable<Reservation> NotDelete(this IQueryable<Reservation> reservations)
  {
    return reservations.Where(reservation => !reservation.IsDeleted);
  }

  public static IQueryable<LandlordReservationDto>
    SelectLandlordReservationDto(this IQueryable<Reservation> reservations)
  {
    return reservations.Select(reservation => new LandlordReservationDto()
    {
      Id = reservation.Id,
      CreateAt = reservation.ModifyAt,
      Status = (ReservationStatus)reservation.Status,
      Tenant =
        new UserResponse()
        {
          Id = reservation.User.Id, Name = reservation.User.UserName!, AvatarUrl = Uri.BuildUrlWithHost(reservation.User.Avatar!.Url),
        },
      Accommodation = new AccommodationResponse()
      {
        Id = reservation.Apartment.Accommodation.Id,
        Title = reservation.Apartment.Accommodation.Title,
        Apartment = new ApartmentResponse() { Id = reservation.Apartment.Id, Name = reservation.Apartment.Name, },
      },
    });
  }
  public static IQueryable<Reservation> RealExist(this IQueryable<Reservation> reservations)
  {
    return reservations.Where(a => a.Apartment.ApartmentBedTypes
                                    .Any(b => b.Type == a.BedType));
  }

  public static IQueryable<TenantReservationDto> SelectTenantReservation(this IQueryable<Reservation> reservations)
  {
    return reservations.Select(reservation=>new TenantReservationDto
    {
      Id = reservation.Id,
      CreateAt = reservation.ModifyAt,
      Status = (ReservationStatus)reservation.Status,
      CheckinDate = reservation.FromDate,
      CheckoutDate = reservation.ToDate,
      Tenant = 
        new UserResponse()
        {
          Id = reservation.UserId, Name = reservation.User.FullName!, Phone = reservation.User.PhoneNumber,
          Email = reservation.User.Email,
        },
      Accommodation =  new AccommodationDetailRes()
      {
        Id = reservation.Apartment.AccommodationId,
        Title = reservation.Apartment.Accommodation.Title,
        Images =
          reservation.Apartment.Accommodation.AccommodationPublications
                     .Select(image => Uri.BuildUrlWithHost(image.Media!.Url))
                                         .Take(5)
                                         .ToList()!,
        Apartment = new ApartmentDetailRes()
        {
          Id = reservation.Apartment.Id,
          Name = reservation.Apartment.Name,
          Price = reservation.Apartment.ApartmentBedTypes.First(b => reservation.BedType == b.Type).Price,
          ThumbnailUrl = Uri.BuildUrlWithHost(reservation.Apartment.ApartmentPublications.First().Media!.Url)!,
          TypeOfBed = reservation.BedType,
        },
      },
    });
  }

  public static IQueryable<ReservationDetailRes> SelectReservationDetailRes(this IQueryable<Reservation> reservations)
  {
    return reservations.Select(reservation => new ReservationDetailRes()
    {
      Id = reservation.Id,
      CreateAt = reservation.ModifyAt,
      Status = reservation.Status,
      CheckinDate = reservation.FromDate,
      CheckoutDate = reservation.ToDate,
      Tenant =
        new UserResponse()
        {
          Id = reservation.User.Id, Name = reservation.User.FullName!, Phone = reservation.User.PhoneNumber,
        },
      Accommodation = new AccommodationDetailRes()
      {
        Id = reservation.Apartment.AccommodationId,
        Title = reservation.Apartment.Accommodation.Title,
        Images =
          reservation.Apartment.Accommodation.AccommodationPublications
                     .Select(image => Uri.BuildUrlWithHost(image.Media!.Url))
                                         .Take(5)
                                         .ToList()!,
        Apartment = new ApartmentDetailRes()
        {
          Id = reservation.Apartment.Id,
          Name = reservation.Apartment.Name,
          Price = reservation.Apartment.ApartmentBedTypes.First(b => reservation.BedType == b.Type).Price,
          ThumbnailUrl = Uri.BuildUrlWithHost(reservation.Apartment.ApartmentPublications.First().Media!.Url)!,
          TypeOfBed = reservation.Apartment.TypeOfBed,
        },
      },
    });
  }
  
}