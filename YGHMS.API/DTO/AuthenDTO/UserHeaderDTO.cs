using YGHMS.API.Infra.Enums;

namespace YGHMS.API.DTO.AuthenDTO
{
    public class UserHeader
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public UserRole Role { get; set; }
        public AccountStatus? Status { get; set; }
        public DateTime Expires { get; set; }
    }
}
