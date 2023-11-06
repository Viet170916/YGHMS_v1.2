using YGHMS.API.Infra.Enums;

namespace YGHMS.API.DTO.ResponseModels.UserDTOs;

public class DisplayOnlyProfileUserDto : UserDTO
{
    public AccountStatus AccountStatus { get; set; }
    public string? Introduce { get; set; }
}