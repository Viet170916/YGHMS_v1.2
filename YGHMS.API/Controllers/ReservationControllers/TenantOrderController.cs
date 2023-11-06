using Microsoft.AspNetCore.Mvc;
using YGHMS.API.Common;
using YGHMS.API.DTO.RequestModels;
using YGHMS.API.DTO.ResponseModels;
using YGHMS.API.DTO.ResponseModels.ReservationDTOs;
using YGHMS.API.Services.ReservationServices;

namespace YGHMS.API.Controllers.ReservationControllers;

[Route("api/booking")]
[ApiController]
public class TenantOrderController : BaseApiController
{
  private readonly ILogger<TenantOrderController> _logger;
  private readonly IReservationService _reservation;

  public TenantOrderController(
    ILogger<TenantOrderController> logger,
    IReservationService reservation
  )
  {
    _logger = logger;
    _reservation = reservation;
  }

  [HttpGet("paging")] public IActionResult LandlordReservations(int page, int? postId, int? status)
  {
    try
    {
      var data = _reservation.GetTenantPagedReservation(UserHeader.UserId, page, postId, status);
      return Ok(data);
    }
    catch (Exception e) { return BadRequest(new { err = "lỗi", }); }
  }

  // [HttpGet("first")] public IActionResult Reservation(int id)
  // {
  //   try { return Ok(_reservation.GetReservationById(UserHeader.UserId, id)); }
  //   catch (Exception e) { return BadRequest(new { err = "lỗi", }); }
  // }

  [HttpPost("update")] public IActionResult UpdateTenantReservation(BookingRequest reservation)
  {
    try
    {
      var result = _reservation.UpdateReservation(reservation.Id, reservation, UserHeader.UserId);
      return Ok(new UpdateResponse<TenantReservationDto>() { Data = result, });
    }
    catch (Exception e) { return BadRequest(new UpdateResponseBase() { Status = false, Message = e.Message, }); }
  }

  [HttpPost("create")] public IActionResult CreateReservation(BookingRequest? reservation)
  {
    try
    {
      var result = _reservation.CreateAReservation(reservation, UserHeader.UserId);
      return Ok(new UpdateResponse<TenantReservationDto>() { Data = result, });
    }
    catch (Exception e)
    {
      Console.Write(e);
      return BadRequest(new UpdateResponseBase() { Status = false, Message = e.Message, });
    }
  }

  [HttpGet("first")] public IActionResult CreateReservation(int reservationId, int status)
  {
    try
    {
      var result = _reservation.GetReservation(reservationId, status, UserHeader?.UserId ?? 11);
      if (result is null)
      {
        return NotFound(new UpdateResponseBase { Status = false, Message = Constants.ErrorMessages.NotFound });
      }

      return Ok(result);
    }
    catch (Exception e) { return BadRequest(new UpdateResponseBase() { Status = false, Message = e.Message, }); }
  }

  // [HttpPost("update/delete")] public IActionResult UpdateDeleteReservation(UpdateStatusReservationBody body)
  // {
  //   try
  //   {
  //     _reservation.DeleteReservation(body.ReservationId, UserHeader.UserId);
  //     return Ok(new UpdateResponseBase());
  //   }
  //   catch (Exception e) { return BadRequest(new UpdateResponseBase() { Status = false, Message = "update fail", }); }
  // }
}