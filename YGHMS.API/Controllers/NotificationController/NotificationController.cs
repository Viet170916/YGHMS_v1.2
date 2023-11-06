using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YGHMS.API.DTO.Common;
using YGHMS.API.DTO.NotificationDTOs;
using YGHMS.API.Services.NotiServices;

namespace YGHMS.API.Controllers.NotificationController
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseApiController
    {
        private readonly INotiService _notiService;
        private readonly ILogger<NotificationController> _logger;
        public NotificationController(INotiService notiService, ILogger<NotificationController> logger)
        {
            _notiService = notiService;
            _logger = logger;
        }
        [HttpGet("get-all-notification")]
        public PagedResultDTO<NotificationDTO> GetAllNoti(int pageSize, int pageIndex)
        {
            if (UserHeader == null) throw new UnauthorizedAccessException();
            return _notiService.GetNotification(UserHeader.UserId, pageSize, pageIndex);
        }
        [HttpPost("read-all")]
        public ResponseDTO<bool> ReadAll()
        {
            if (UserHeader == null) throw new UnauthorizedAccessException();
            return _notiService.ReadAll(UserHeader.UserId);
        }
        [HttpPost("read-noti")]
        public ResponseDTO<bool> ReadNoti(int notificationId)
        {
            if (UserHeader == null) throw new UnauthorizedAccessException();
            return _notiService.ReadNotification(UserHeader.UserId, notificationId);
        }
        [HttpPost("delete-noti")]
        public ResponseDTO<bool> DeleteNotification(int notificationId)
        {
            if (UserHeader == null) throw new UnauthorizedAccessException();
            return _notiService.DeleteNotification(UserHeader.UserId, notificationId);
        }
    }
}
