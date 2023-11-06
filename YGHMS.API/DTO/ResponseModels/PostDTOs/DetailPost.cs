using YGHMS.API.DTO.ResponseModels.AddressDTOs;
using YGHMS.API.DTO.ResponseModels.ApartmentDTOs;
using YGHMS.API.DTO.ResponseModels.PostDTOs.TypeModels;

namespace YGHMS.API.DTO.ResponseModels.PostDTOs;

public class DetailPostResponse
{
  public int Id { get; set; }
  public string? Title { get; set; }
  public decimal? Quality { get; set; }
  public string? Description { get; set; }
  public PostLocation? Location { get; set; }
  public IList<ImageResponse>? Images { get; set; }
  public EstateTypeResponse? EstateType { get; set; }
  public PriceRangeResponse? PriceRange { get; set; }
  public string? Policies { get; set; }
  public ReviewDetailResponse? Review { get; set; }
  public IList<AmenityResponse>? Amenities { get; set; }
  public IList<FilterAmenityResponse>? FilterAmenities { get; set; }
  public OwnerResponse? Owner { get; set; }
  public IList<ReviewResponse>? Reviews { get; set; }
}

public class ImageResponse
{
  public int? Id { get; set; }
  public string? Url { get; set; }
  public string? Description { get; set; }
}

public class EstateTypeResponse
{
    public int? Id { get; set; }
    public string? Name { get; set; }
}

public class ReviewDetailResponse
{
  public int? Count { get; set; }
  public decimal? Rate { get; set; }
  public string? Description { get; set; }
}

public class AmenityResponse
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public int? RoomCount { get; set; }
}

public class FilterAmenityResponse
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public int? NumberOfAvailableApartment { get; set; }
}

public class OwnerResponse
{
  public int Id { get; set; }
  public string? UserName { get; set; }
  public string? About { get; set; }
  public string? AvatarUrl { get; set; }
  public string? CoverImageUrl { get; set; }
  public int? Count { get; set; }
  public int? ApartmentCount { get; set; }
  public IList<HighLightResponse>? Highlight { get; set; }
  public DateTime? CreateAt { get; set; }
}

public class HighLightResponse
{
  public int? Type { get; set; }
  public string? Description { get; set; }
  public DateTime? CreateAt { get; set; }
}

public class ReviewResponse
{
  public decimal Rate { get; set; }
  public int Id { get; set; }
  public string? Title { get; set; }
  public int Type { get; set; }
  public string? Content { get; set; }
  public bool IsRent { get; set; }
  public DateTime? CreateAt { get; set; }
  public UserResponse? User { get; set; }
  public ReservationResponse? Reservation { get; set; }
}

public class UserResponse
{
  public int Id { get; set; }
  public string? Username { get; set; }
  public string? AvatarUrl { get; set; }
}

public class ReservationResponse
{
  public int? Id { get; set; }
  public TimeResponse? Time { get; set; }
  public ApartmentResponse? Apartment { get; set; }
}

public class TimeResponse
{
  public int? Night { get; set; }
  public string? InMonth { get; set; }
}