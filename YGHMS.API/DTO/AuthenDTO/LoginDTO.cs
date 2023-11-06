using YGHMS.API.Infra.Enums;

namespace YGHMS.API.DTO.AuthenDTO
{
    public class LoginRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public UserRole Role { get; set; }
        public AccountStatus Status { get; set; }
    }
}
