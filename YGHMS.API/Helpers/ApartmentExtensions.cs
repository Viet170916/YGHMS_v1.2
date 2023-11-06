
using YGHMS.API.Common;
using YGHMS.API.DTO.ResponseModels.ApartmentDTOs;
using YGHMS.API.DTO.ResponseModels.PostDTOs;
using YGHMS.API.DTO.ResponseModels.PostDTOs.FilterResponse;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Infra.Models;
using Timer = YGHMS.API.Common.Timer;
using Uri = YGHMS.API.Common.Uri;

namespace YGHMS.API.Helpers;

public static class ApartmentExtensions
{
  public static IQueryable<AmenityResponse> SelectAmenityResponse(
    this IEnumerable<ApartmentsAmenity> amenities,
    AmenityType type
  )
  {
    return amenities.Where(a => a.Amenity.Type == (int)type)
                    .Select(a => new AmenityResponse() { Id = a.AmenityId, Name = a.Amenity.Name, })
                    .AsQueryable();
  }

  public static IQueryable<ApartmentDetailResponse> SelectApartmentDetailResponses(
    this IQueryable<Apartment> apartments
  )
  {
    return apartments
      .Select(a => new ApartmentDetailResponse()
      {
        Id = a.Id,
        Name = a.Name,
        Area = a.Area,
        Available = a.ApartmentBedTypes.Select(av => av.Quantity).Sum(),
        Description = a.Description,
        AmenitiesRoom = a.ApartmentsAmenities
                         .Where(a => a.Amenity.Type == (int)AmenityType.ROOM)
                         .Select(a => new AmenityResponse() { Id = a.AmenityId, Name = a.Amenity.Name, })
                         .ToList(),
        AmenitiesPayment = a.ApartmentsAmenities
                            .Where(a => a.Amenity.Type == (int)AmenityType.PAYMENT)
                            .Select(a => new AmenityResponse() { Id = a.AmenityId, Name = a.Amenity.Name, })
                            .ToList(),
        NumberOfReservation = a.Reservations.Count(),
        Images = a.ApartmentPublications
                  .Select(img => new ImageResponse()
                  {
                    Id = img.MediaId, Description = img.Media!.Description, Url = Uri.BuildUrlWithHost(img.Media.Url),
                  })
                  .ToList(),
        Amenities = a.ApartmentsAmenities
                     .Where(a => a.Amenity.Type == (int)AmenityType.FACILITY)
                     .Select(a => new AmenityResponse() { Id = a.AmenityId, Name = a.Amenity.Name, })
                     .ToList(),
        Benefits = a.ApartmentsAmenities
                    .Where(a => a.Amenity.Type == (int)AmenityType.PAYMENT)
                    .Select(a => new BenefitResponse { Id = a.AmenityId, Name = a.Amenity.Name, })
                    .ToList(),
        BedTypes = a.ApartmentBedTypes.Select(b => new BedTypeDetailResponse()
                    {
                      Id = b.Id,
                      Type = b.Type,
                      Price = b.Price,
                      Quantity = b.Quantity,
                      UnAvailableDates =
                        Timer.MergeTimeRanges(Timer.GetMostFrequentOverlappingTimeRanges(b
                                 .Apartment
                                 .Reservations
                                 .Where(r => r.BedType == b.Type)
                                 .Where(r => !r.IsDeleted
                                             &&
                                             r.ToDate >= DateTime.Today &&
                                             r.Status != (int)ReservationStatus.DRAFT&&
                                             r.Status != (int)ReservationStatus.DONE &&
                                             r.Status != (int)ReservationStatus.CANCELED &&
                                             r.Status != (int)ReservationStatus.REJECTED)
                                 .Select(r =>new TimeRange() { Since = r.FromDate, To = r.ToDate, })
                                 .ToList()
                               , b.Quantity)),
                    })
                    .ToList(),
      });
  }
  public static IQueryable<ApartmentDetailResponse> SelectApartmentDetailResponses_Shorten(
    this IQueryable<Apartment> apartments
  )
  {
    return apartments
      .Select(a => new ApartmentDetailResponse()
      {
        Id = a.Id,
        Name = a.Name,
        PostId = a.AccommodationId,
        UserName = a.Owner.UserName,
        Images = a.ApartmentPublications
                  .Select(img => new ImageResponse()
                  {
                    Id = img.MediaId, Description = img.Media!.Description, Url = Uri.BuildUrlWithHost(img.Media.Url),
                  })
                  .ToList(),
        BedTypes = a.ApartmentBedTypes.Select(b => new BedTypeDetailResponse()
                    {
                      Id = b.Id,
                      Type = b.Type,
                      Price = b.Price,
                      Quantity = b.Quantity,
                      UnAvailableDates =
                        Timer.MergeTimeRanges(Timer.GetMostFrequentOverlappingTimeRanges(b
                                 .Apartment
                                 .Reservations
                                 .Where(r => r.BedType == b.Type)
                                 .Where(r => !r.IsDeleted
                                             &&
                                             r.ToDate >= DateTime.Today &&
                                             r.Status != (int)ReservationStatus.DRAFT&&
                                             r.Status != (int)ReservationStatus.DONE &&
                                             r.Status != (int)ReservationStatus.CANCELED &&
                                             r.Status != (int)ReservationStatus.REJECTED)
                                 .Select(r => new TimeRange { Since = r.FromDate, To = r.ToDate, })
                                 .ToList()
                               , b.Quantity)),
                    })
                    .ToList(),
      });
  }
}