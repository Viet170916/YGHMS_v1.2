using YGHMS.API.DTO.ResponseModels.ApartmentDTOs;
using YGHMS.API.DTO.ResponseModels.PostDTOs;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Infra.Models;
using Uri = YGHMS.API.Common.Uri;

namespace YGHMS.API.Helpers;

public static class ReviewExtension
{
  public static IEnumerable<ReviewResponse> SelectReviewResponses(this IEnumerable<Review> reviews)
  {
    return reviews.Select(a => new ReviewResponse
    {
      Id = a.Id,
      IsRent = a.Reservation.Status == (int)ReservationStatus.DONE,
      CreateAt = a.CreateAt,
      Content = a.Comment,
      User =
        new UserResponse { Id = a.UserId, Username = a.User.UserName, AvatarUrl = Uri.BuildUrlWithHost(a.User.Avatar!.Url), },
      Reservation = new ReservationResponse
      {
        Id = a.ReservationId,
        Apartment = new ApartmentResponse
        {
          Id = a.Reservation.ApartmentId, BedType = a.Reservation.BedType, Name = a.Reservation.Apartment.Name,
        },
        Time = new TimeResponse
        {
          Night = a.Reservation.ToDate.Day - a.Reservation.FromDate.Day,
          InMonth = a.Reservation.FromDate.ToShortDateString(),
        },
      },
      Rate = a.Rate,
    });
  }
}