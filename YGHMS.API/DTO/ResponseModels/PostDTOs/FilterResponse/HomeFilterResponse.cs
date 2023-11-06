using YGHMS.API.DTO.ResponseModels.AddressDTOs;
using YGHMS.API.DTO.ResponseModels.PostDTOs.TypeModels;

// ReSharper disable All
namespace YGHMS.API.DTO.ResponseModels.PostDTOs.FilterResponse;
public class TimeRange
{
  public DateTime? Since { get; set; }
  public DateTime? To { get; set; }
}
public class HomeFilterResponse
{
  public PostLocation? Location { get; set; }
  public TimeRange? AvailableDate { get; set; }
  public PriceRangeResponse? Price { get; set; }
  public int? NumberOfBed { get; set; }
  public EstateType? EstateType { get; set; }
  public IList<AmenityFilter>? Amenities { get; set; }
}

public class EstateType
{
  public int Id { get; set; }
  public string? Name { get; set; }
}

public class AmenityFilter
{
  public int Id { get; set; }
  public string? Name { get; set; }
}

