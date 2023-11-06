namespace YGHMS.API.DTO.ResponseModels.UserDTOs;

public class EditableProfileUserDto : DisplayOnlyProfileUserDto
{
    public string? Address { get; set; }

    public Contact? Contact { get; set; }

}

public class Contact
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}