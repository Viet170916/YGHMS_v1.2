using YGHMS.API.DTO.ResponseModels.PostDTOs;
using YGHMS.API.DTO.ResponseModels.PostDTOs.FilterResponse;

namespace YGHMS.API.DTO.ResponseModels.ApartmentDTOs;

public class ApartmentResponse
{
  public int? Id { get; set; }
  public string? Name { get; set; }
  public int? BedType { get; set; }
}

public class DetailApartmentResponse
{
  public int? Id { get; set; }
  public string? Name { get; set; }
  public int? BedType { get; set; }
}

public class ApartmentDetailResponse
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public string? UserName { get; set; }
  public int? PostId { get; set; }
  public string? Description { get; set; }
  public float? Area { get; set; }
  public int? Available { get; set; }
  public int? NumberOfReservation { get; set; }
  public IList<AmenityResponse>? AmenitiesPayment { get; set; }
  public IList<AmenityResponse>? AmenitiesRoom { get; set; }
  public IList<ImageResponse>? Images { get; set; }
  public IList<AmenityResponse>? Amenities { get; set; }
  public IList<BedTypeDetailResponse>? BedTypes { get; set; }
  public IList<BenefitResponse>? Benefits { get; set; }
}

public class BedTypeDetailResponse
{
  public int Id { get; set; }
  public int Type { get; set; }
  public double? Price { get; set; }
  public int? Quantity { get; set; }
  public IList<TimeRange>? UnAvailableDates { get; set; }
}

public class BenefitResponse
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public string? ApplyingUntilDate { get; set; }
}