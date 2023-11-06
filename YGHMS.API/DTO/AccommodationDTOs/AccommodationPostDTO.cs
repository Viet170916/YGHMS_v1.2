using YGHMS.API.DTO.ResponseModels.AddressDTOs;
using YGHMS.API.DTO.ResponseModels.UserDTOs;

namespace YGHMS.API.DTO.AccommodationDTOs;

public class AccommodationPostDTO
{
  public int Id { get; set; }
  public UserDTO Owner { get; set; }
  public string EstateType { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public decimal Quanlity { get; set; }
  public LocationDTO Location { get; set; }
  public string? Policies { get; set; }
  public DateTime Expiration { get; set; }
}