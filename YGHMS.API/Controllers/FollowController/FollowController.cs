using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YGHMS.API.DTO.Common;
using YGHMS.API.Services.FollowServices;

namespace YGHMS.API.Controllers.FollowController
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : BaseApiController
    {
        private readonly ILogger<FollowController> _logger;
        private readonly IFollowService _followService;
        public FollowController(ILogger<FollowController> logger, IFollowService followService)
        {
            _logger = logger;
            _followService = followService;
        }
        [HttpPost("follow-post")]
        public ResponseDTO<bool> FollowPost(int postId)
        {
            if(UserHeader == null)
            {
                throw new UnauthorizedAccessException();
            }
            int userId = UserHeader.UserId;
            return _followService.FollowPost(userId, postId);
        }
        [HttpPost("unfollow-post")]
        public ResponseDTO<bool> UnFollowPost(int postId)
        {
            if (UserHeader == null)
            {
                throw new UnauthorizedAccessException();
            }
            int userId = UserHeader.UserId;
            return _followService.UnFollowPost(userId, postId);
        }
    }
}
