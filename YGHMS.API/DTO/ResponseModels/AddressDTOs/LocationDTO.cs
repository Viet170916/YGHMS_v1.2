namespace YGHMS.API.DTO.ResponseModels.AddressDTOs;

public class LocationDTO
{
  public string Latitude { get; set; }
  public string Longitude { get; set; }
  public AddressDto Address { get; set; }
}