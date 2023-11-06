using Microsoft.EntityFrameworkCore;
using YGHMS.API.DTO.ResponseModels.AddressDTOs;
using YGHMS.API.DTO.ResponseModels.ApartmentDTOs;
using YGHMS.API.DTO.ResponseModels.PostDTOs;
using YGHMS.API.DTO.ResponseModels.PostDTOs.FilterResponse;
using YGHMS.API.DTO.ResponseModels.PostDTOs.TypeModels;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Infra.Models;
using Uri = YGHMS.API.Common.Uri;
using UserResponse = YGHMS.API.DTO.ResponseModels.PostDTOs.UserResponse;

namespace YGHMS.API.Helpers;

public static class PostExtension
{
  public static IQueryable<Accommodation> Expired(this IQueryable<Accommodation> accommodations)
  {
    return accommodations.Where(post => post.Expiration < DateTime.Today);
  }

  public static IQueryable<Accommodation> Draft(this IQueryable<Accommodation> accommodations)
  {
    return accommodations.Where(post => post.Status == (int)PostStatus.DRAFT);
  }

  public static IQueryable<Accommodation> Rejected(this IQueryable<Accommodation> accommodations)
  {
    return accommodations.Where(post => post.Status == (int)PostStatus.REJECTED);
  }

  public static IQueryable<Accommodation> Approved(this IQueryable<Accommodation> accommodations)
  {
    return accommodations.Where(post => post.Status == (int)PostStatus.APPROVED);
  }

  public static IQueryable<Accommodation> Deleted(this IQueryable<Accommodation> accommodations)
  {
    return accommodations.Where(post => post.Status == (int)PostStatus.DELETED);
  }

  public static IQueryable<Accommodation> StillActivated(this IQueryable<Accommodation> accommodations)
  {
    return accommodations.Where(post => post.Expiration >= DateTime.Today && post.Status == (int)PostStatus.ACTIVATED);
  }

  public static IQueryable<Accommodation> AreWaiting(this IQueryable<Accommodation> accommodations)
  {
    return accommodations.Where(post => post.Status == (int)PostStatus.WAITING);
  }

  public static IQueryable<Accommodation> Private(
    this IQueryable<Accommodation> accommodations,
    int userId,
    bool deleted = false
  )
  {
    return accommodations.Where(post => post.IsDeleted == deleted && post.OwnerId == userId);
  }

  public static IQueryable<Accommodation> IsNotDeletedPost(this IQueryable<Accommodation> post)
  {
    return post.Where(accommodation => !accommodation.IsDeleted);
  }

  public static IQueryable<Accommodation> IsPriceFilterNotNull(
    this IQueryable<Accommodation> activePost,
    PriceRangeResponse priceRange
  )
  {
    if (priceRange.To < priceRange.Max)
      activePost = activePost.Where(post =>
        post.Apartments
            .Any(apartment => apartment.Price < priceRange.To));
    return activePost
      .Where(post => post.Apartments
                         .Any(apartment => apartment.Price > priceRange.From));
  }

  public static IQueryable<Accommodation> IsDateRangeNotNull(
    this IQueryable<Accommodation> activePost,
    TimeRange timeRange
  )
  {
    return activePost.Where(post => post.Apartments
                                        .Any(a => a.Reservations
                                                   .Any(reservation =>
                                                     reservation.FromDate < timeRange.To ||
                                                     reservation.ToDate < timeRange.Since)));
  }

  public static IQueryable<Accommodation> IsAmenitiesNotNull(
    this IQueryable<Accommodation> activePost,
    IList<AmenityFilter> amenities
  )
  {
    var amenityIds = amenities.Select(amenity => amenity.Id).ToList();
    return activePost
      .Where(post => post.Apartments
                         .Any(a => a.ApartmentsAmenities
                                    .Any(apartmentsAmenity => amenityIds.Contains(apartmentsAmenity.AmenityId))));
  }

  public static IQueryable<PostDisplayAsListDto>
    SelectPostDisplayAsListQueryable(this IQueryable<Accommodation> postDisplayAsList, int? userId)
  {
    var result = postDisplayAsList.Select(acom => new PostDisplayAsListDto()
    {
      Id = acom.Id,
      Image = Uri.BuildUrlWithHost(acom.AccommodationPublications.First().Media!.Url),
      Title = acom.Title,
      Quality = acom.Quality,
      UserName = acom.Owner.UserName,
      Location = new PostLocation()
      {
        Id = acom.Address!.Id,
        City = acom.Address.City,
        District = acom.Address.District,
        Commune = acom.Address.Commune,
        Latitude = acom.Latitude,
        Longitude = acom.Longitude,
      },
      Price = new PriceRangeResponse()
      {
        From = acom.Apartments.SelectMany(ap => ap.ApartmentBedTypes).Min(apartment => apartment.Price),
        To = acom.Apartments.SelectMany(ap => ap.ApartmentBedTypes).Max(apartment => apartment.Price),
      },
      IsFollowed = acom.FollowUserAccoms
                       .Any(accom => accom.UserId == userId && !accom.IsDeleted),
      Follower = new FollowerInPostList()
      {
        Count = acom
                .FollowUserAccoms
                .Count,
        FiveTaken = acom.FollowUserAccoms
                        .Where(ac => !ac.IsDeleted)
                        // .OrderBy(a => random.Next())
                        .Take(3)
                        .Select(followUserAccom =>
                          new Taken()
                          {
                            Avatar = Uri.BuildUrlWithHost(followUserAccom.User.Avatar!.Url),
                            Name = followUserAccom.User.UserName,
                          })
                        .ToList(),
      },
      Reviews = new ReviewInPostList()
      {
        Count = acom.Apartments.Sum(a => a.Reservations.Sum(r => r.Reviews.Count)),
        FiveTaken = acom.Apartments
                        .SelectMany(a => a.Reservations)
                        .SelectMany(a => a.Reviews)
                        .Take(3)
                        .Select(review =>
                          new Taken()
                          {
                            Avatar = Uri.BuildUrlWithHost(review.User.Avatar!.Url), Name = review.User.UserName,
                          })
                        .ToList(),
        Rate = acom.Apartments
                   .SelectMany(a => a.Reservations)
                   .SelectMany(a => a.Reviews)
                   .Average(review => review.Rate),
      },
    });
    return result;
  }

  public static IQueryable<DetailPostResponse> SelectDetailPostResponse(this IQueryable<Accommodation> post)
  {
    var result = post
                 .Include(p => p.Address)
                 .Include(p => p.EstateTypes)
                 .Include(p => p.Apartments)
                 .ThenInclude(a => a.ApartmentBedTypes)
                 .Include(p => p.AccommodationPublications)
                 .Include(p => p.Owner)
                 .Select(p => new DetailPostResponse
                 {
                   Id = p.Id,
                   Title = p.Title,
                   Quality = p.Quality,
                   Description = p.Description,
                   Review = new ReviewDetailResponse()
                   {
                     Count = p.Apartments
                              .SelectMany(c => c.Reservations)
                              .SelectMany(c => c.Reviews)
                              .Count(),
                     Rate = p.Apartments
                             .SelectMany(c => c.Reservations)
                             .SelectMany(c => c.Reviews)
                             .Average(a => a.Rate),
                   },
                   Location = new PostLocation
                   {
                     Id = p.Address!.Id,
                     City = p.Address.City,
                     District = p.Address.District,
                     Commune = p.Address.Commune,
                     Latitude = p.Latitude,
                     Longitude = p.Longitude,
                   },
                   Images =
                     p.AccommodationPublications.Select(img => new ImageResponse()
                      {
                        Id = img.MediaId,
                        Url = Uri.BuildUrlWithHost(img.Media!.Url),
                        Description = img.Media.Description,
                      })
                      .ToList(),
                   EstateType = new EstateTypeResponse { Id = p.EstateTypesId, Name = p.EstateTypes.Name, },
                   PriceRange = new PriceRangeResponse
                   {
                     From = p.Apartments.SelectMany(a => a.ApartmentBedTypes).Min(a => a.Price),
                     To = p.Apartments.SelectMany(a => a.ApartmentBedTypes).Max(a => a.Price),
                   },
                   Policies = p.Policies,
                   Amenities = p.Apartments.SelectMany(a => a.ApartmentsAmenities)
                                .Where(am => am.Amenity.Type == (int)AmenityType.ROOM)
                                .GroupBy(g => g.Amenity.Id)
                                .Take(3)
                                .Select(am => new AmenityResponse
                                {
                                  Id = am.Key, Name = am.First().Amenity.Name, RoomCount = am.Count(),
                                })
                                .ToList(),
                   FilterAmenities = p.Apartments
                                      .SelectMany(a => a.ApartmentsAmenities)
                                      .GroupBy(a => a.Amenity.Id)
                                      .Select(a => new FilterAmenityResponse
                                      {
                                        Id = a.Key,
                                        Name = a.FirstOrDefault()!.Amenity.Name,
                                        NumberOfAvailableApartment = a.Count(),
                                      })
                                      .Distinct()
                                      .ToList(),
                   Owner = new OwnerResponse
                   {
                     Id = p.OwnerId,
                     UserName = p.Owner.UserName,
                     AvatarUrl = Uri.BuildUrlWithHost(p.Owner.Avatar!.Url),
                     Count = p.Owner.Accommodations.Count,
                     ApartmentCount = p.Owner.Accommodations
                                       .SelectMany(a => a.Apartments)
                                       .Count(),
                     Highlight = p.Owner.UserHighlights
                                  .Select(highlight => new HighLightResponse
                                  {
                                    Type = highlight.Title, Description = p.Description, CreateAt = p.CreateAt,
                                  })
                                  .ToList(),
                     CreateAt = p.Owner.CreateAt,
                   },
                   Reviews = p.Apartments
                              .SelectMany(a => a.Reservations)
                              .SelectMany(a => a.Reviews)
                              .OrderByDescending(a => a.CreateAt)
                              .Take(10)
                              .Select(a => new ReviewResponse
                              {
                                Id = a.Id,
                                IsRent = a.Reservation.Status == (int)ReservationStatus.DONE,
                                CreateAt = a.CreateAt,
                                Content = a.Comment,
                                User =
                                  new UserResponse
                                  {
                                    Id = a.UserId,
                                    Username = a.User.UserName,
                                    AvatarUrl = Uri.BuildUrlWithHost(a.User.Avatar!.Url),
                                  },
                                Reservation = new ReservationResponse
                                {
                                  Id = a.ReservationId,
                                  Apartment = new ApartmentResponse
                                  {
                                    Id = a.Reservation.ApartmentId,
                                    BedType = a.Reservation.BedType,
                                    Name = a.Reservation.Apartment.Name,
                                  },
                                  Time = new TimeResponse
                                  {
                                    Night = a.Reservation.ToDate.Day - a.Reservation.FromDate.Day,
                                    InMonth = a.Reservation.FromDate.ToShortDateString(),
                                  },
                                },
                                Rate = a.Rate,
                              })
                              .ToList(),
                 });
    return result;
  }

  public static IEnumerable<ReviewResponse> SelectReviewsAsReviewResponse(this IEnumerable<Review> a)
  {
    return a.Select(a => new ReviewResponse()
    {
      Id = a.Id,
      IsRent = a.Reservation.Status == (int)ReservationStatus.DONE,
      CreateAt = a.CreateAt,
      Content = a.Comment,
      User =
        new UserResponse()
        {
          Id = a.UserId, Username = a.User.UserName, AvatarUrl = Uri.BuildUrlWithHost(a.User.Avatar!.Url),
        },
      Reservation = new ReservationResponse()
      {
        Id = a.ReservationId,
        Apartment = new ApartmentResponse()
        {
          Id = a.Reservation.ApartmentId, BedType = a.Reservation.BedType, Name = a.Reservation.Apartment.Name,
        },
        Time = new TimeResponse()
        {
          // Night = a.Reservation.ToDate.Day - a.Reservation.FromDate.Day,
          // InMonth = a.Reservation.FromDate,
        },
      },
      Rate = a.Rate,
    });
  }
}