using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YGHMS.API.Common;
using YGHMS.API.DTO.RequestModels;
using YGHMS.API.DTO.ResponseModels;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Services.ReservationServices;

namespace YGHMS.API.Controllers.ReservationControllers;

[Route("api/order")]
[ApiController]
public class LandlordReservationController : BaseApiController
{
    private readonly ILogger<LandlordReservationController> _logger;
    private readonly IReservationService _landlordReservation;

    public LandlordReservationController(
      ILogger<LandlordReservationController> logger,
      IReservationService landlordReservation
    )
    {
        _logger = logger;
        _landlordReservation = landlordReservation;
    }

    [HttpGet("paging")]
    public IActionResult LandlordReservations(int page, int? postId, int? status)
    {
        try
        {
            var data = _landlordReservation.GetPagedReservation(UserHeader.UserId, page, postId, status);
            return Ok(data);
        }
        catch (Exception e) { return BadRequest(new { err = "lỗi", }); }
    }

    [HttpGet("first")]
    public IActionResult Reservation(int id)
    {
        try { return Ok(_landlordReservation.GetReservationById(UserHeader.UserId, id)); }
        catch (Exception e) { return BadRequest(new { err = "lỗi", }); }
    }

    [HttpPost("update/status")]
    public IActionResult UpdateStatusReservation(UpdateStatusReservationBody body)
    {
        try
        {
            //cần xử lý thêm trường hợp ràng buộc cho thứ tự status thì mới được thay đổi status 
            if (body.Status is null || !EnumIncluded.IsIncluded<ReservationStatus>((ReservationStatus)body.Status))
                return BadRequest(new UpdateResponseBase() { Status = false, Message = "update fail", });
            _landlordReservation.SetStatusToReservation(body.ReservationId, UserHeader.UserId, body.Status);
            return Ok(new UpdateResponseBase());
        }
        catch (Exception e) { return BadRequest(new UpdateResponseBase() { Status = false, Message = "update fail", }); }
    }

    [HttpPost("update/delete")]
    public IActionResult UpdateDeleteReservation(UpdateStatusReservationBody body)
    {
        try
        {
            _landlordReservation.DeleteReservation(body.ReservationId, UserHeader.UserId);
            return Ok(new UpdateResponseBase());
        }
        catch (Exception e) { return BadRequest(new UpdateResponseBase() { Status = false, Message = "update fail", }); }
    }
}