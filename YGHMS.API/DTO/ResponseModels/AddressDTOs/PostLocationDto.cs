namespace YGHMS.API.DTO.ResponseModels.AddressDTOs;

public class PostLocation
{
  public int? Id { get; set; }
  public string? Country { get; set; }
  public string? City { get; set; }
  public string? District { get; set; }
  public string? Commune { get; set; }

  public string DisplayName =>
    City + (District != null ? ", " : "") +
    District + (Commune != null ? ", " : "") +
    Commune;

  public double? Longitude { get; set; }
  public double? Latitude { get; set; }
}