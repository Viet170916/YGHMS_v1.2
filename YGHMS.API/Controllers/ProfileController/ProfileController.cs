using Microsoft.AspNetCore.Mvc;
using YGHMS.API.DTO.ResponseModels;
using YGHMS.API.DTO.ResponseModels.UserDTOs;
using YGHMS.API.Infra.Const;
using YGHMS.API.Services.AccountServices;

namespace YGHMS.API.Controllers.ProfileController
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : BaseApiController
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IProfileService _profileServices;

        public ProfileController(ILogger<ProfileController> logger, IProfileService profileServices)
        {
            _logger = logger;
            _profileServices = profileServices;
        }

        [HttpGet("private")]
        public IActionResult GetPersonalProfile()
        {
            try
            {

                if (UserHeader is null)
                {
                    return Unauthorized();
                }
                else
                    return Ok(_profileServices.GetEditableProfile(UserHeader.UserId));
            }
            catch (Exception ex)
            {
                return BadRequest(new UpdateResponseBase { Status = false, Message = ex.Message });
            }
        }
        [HttpGet("user-profile")]
        public IActionResult GetDisplayOnlyProfile(int id)
        {
            try
            {
                if (UserHeader is null)
                {
                    return Unauthorized();
                }
                return Ok(_profileServices.GetDisplayOnlyProfileById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new UpdateResponseBase { Status = false, Message = ex.Message });
            }
        }

        [HttpPost("update-profile")]
        public IActionResult UpdateProfile(EditableProfileUserDto updatedProfile)
        {
            try
            {
                if (UserHeader is null) return Unauthorized();
                _profileServices.UpdateProfile(UserHeader.UserId, updatedProfile);
                return Ok(new UpdateResponseBase { Status = true, Message = ResponseMessage.UPDATE_SUCCESSFULLY });
            }
            catch (Exception e)
            {
                return BadRequest(new UpdateResponseBase { Status = false, Message = e.Message });
            }
        }


    }
}
