using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YGHMS.API.DTO.AuthenDTO;
using YGHMS.API.DTO.Common;
using YGHMS.API.Infra.Const;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Services.AuthenServices;

namespace YGHMS.API.Controllers.AuthenticationController
{
    [Route("api")]
    [ApiController]
    public class RegisterController : BaseApiController
    {
        private readonly IAuthenService _service;
        private readonly ILogger<RegisterController> _logger;
        public RegisterController(IAuthenService service, ILogger<RegisterController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("register")]
        public ResponseDTO<RegisterResponseDTO> Register([FromBody]RegisterResquestDTO request)
        {
            return _service.Register(request);
        }
        [HttpPost("input-username")]
        public ResponseDTO<LoginResponseDTO> InputUsername(string username)
        {
            if (UserHeader == null || UserHeader.Status != AccountStatus.InputNickname) return new ResponseDTO<LoginResponseDTO>((int)RESPONSE_CODE.BadRequest, ResponseMessage.COMMON_ERROR);
            return _service.InputNickname(UserHeader.UserId, username);
        }
    }
}
