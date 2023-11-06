namespace YGHMS.API.DTO.ResponseModels.UserDTOs;

public class UserDTO
{
    public int UserId { get; set; }
    public string? AvatarUrl { get; set; }

    public string? BackgroundUrl { get; set; }
    public string UserName { get; set; }="";
    public string? FullName { get; set; }
   

}