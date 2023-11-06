namespace YGHMS.API.DTO.ResponseModels.AddressDTOs;

public class AddressDto
{
  public int? Id { get; set; }
  public static string? Country { get; set; }
  public string? City { get; set; }
  public string? District { get; set; }
  public string? Commune { get; set; }
  public string? Detail { get; set; }

  public string DisplayName =>
    City + (District != null ? ", " : "") +
    District + (Commune != null ? ", " : "") +
    Commune;
}