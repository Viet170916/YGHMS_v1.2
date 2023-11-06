namespace YGHMS.API.DTO.AdressDTOs;

public class AddressDTO
{
  public int Id { get; }
  public string? Country { get; set; }
  public string? City { get; set; }
  public string? District { get; set; }
  public string? Commune { get; set; }
  public string? Detail { get; set; }
}