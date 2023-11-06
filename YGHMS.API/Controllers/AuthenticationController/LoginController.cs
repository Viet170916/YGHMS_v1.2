using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YGHMS.API.DTO.AuthenDTO;
using YGHMS.API.DTO.Common;
using YGHMS.API.Services.AuthenServices;

namespace YGHMS.API.Controllers.AuthenticationController
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IAuthenService _authenService;
        public LoginController(ILogger<LoginController> logger, IAuthenService authenService)
        {
            _logger = logger;
            _authenService = authenService;
        }

        [HttpPost("login")]
        public ResponseDTO<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            return _authenService.Login(request);
        }
    }
}
