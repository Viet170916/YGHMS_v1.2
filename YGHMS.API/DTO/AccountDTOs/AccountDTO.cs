using YGHMS.API.Infra.Enums;

namespace YGHMS.API.DTO.AccountDTOs
{
    public class AccountDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public double Balance { get; set; } = 0;
        public string Email { get; set; } = "";
        public UserRole Role { get; set; }
        public AccountStatus AccountStatus { get; set; }
    }
    public class ChangePasswordRequestDTO
    {
        public string CurrentPassword { get; set; } = "";
        public string NewPassword { get; set; } = "";
        public string RePassword { get; set; } = "";
    }
    public class UpdateAccountDTO
    {
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Introduce { get; set; }
    }
}
