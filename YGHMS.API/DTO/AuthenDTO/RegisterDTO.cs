namespace YGHMS.API.DTO.AuthenDTO
{
    public class RegisterResquestDTO
    {
        public string Gmail { get;set; }
        public string Password { get;set; }

        public string RePassword { get; set; }
    }
    public class RegisterResponseDTO
    {
        public string Token { get; set; }
        public string UsernameSuggest { get; set; }
    }
    public class InputNickNameResponseDTO
    {

    }
}
