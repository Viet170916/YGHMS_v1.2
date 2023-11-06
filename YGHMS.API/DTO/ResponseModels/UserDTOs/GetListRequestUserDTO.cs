namespace YGHMS.API.DTO.ResponseModels.UserDTOs;

public class GetListRequestUserDTO
{
  public int UserId { get; set; }
  public string Name { get; set; }
  public string? PhoneNumber { get; set; }
  public string? AvatarUrl { get; set; }
}