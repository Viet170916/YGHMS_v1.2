using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YGHMS.API.DTO.RequestModels;
using YGHMS.API.DTO.ResponseModels;
using YGHMS.API.DTO.ResponseModels.PostDTOs.FilterResponse;
using YGHMS.API.Services.ApartmentServices;
using Timer = YGHMS.API.Common.Timer;

namespace YGHMS.API.Controllers.PostingController;

[Route("/api/apartment")]
[ApiController]
public class ApartmentController : BaseApiController
{
  private readonly ILogger<ApartmentController> _logger;
  private readonly IApartmentService _apartmentService;

  public ApartmentController(
    ILogger<ApartmentController> logger,
    IApartmentService apartmentService
  )
  {
    _logger = logger;
    _apartmentService = apartmentService;
  }

  //...
  [HttpGet("public")] public IActionResult GetApartmentsOfAPost(string user, int postId)
  {
    try
    {
      var result =
        _apartmentService
          .GetApartmentDetailResponses(user, postId);
      return Ok(result);
    }
    catch (Exception e) { return BadRequest(new { error = "error", }); }
  }

  [HttpGet("for-booking")] public IActionResult GetApartmentsOfAPost(int status, int reservationId)
  {
    try
    {
      var result = _apartmentService
        .GetApartmentShortenResponsesForReservation(UserHeader.UserId, reservationId, status);
      return Ok(result);
    }
    catch (Exception e) { return BadRequest(new { error = "error", }); }
  }

  [HttpPost("add")] public IActionResult AddApartment(ApartmentRequest apartmentRequest)
  {
    try
    {
      var result = _apartmentService.AddApartment(apartmentRequest, UserHeader!.UserId);
      return Ok(result);
    }
    catch (Exception e) { return BadRequest(new UpdateResponseBase { Message = e.Message, Status = false, }); }
  }[HttpGet("get-draft")] public IActionResult GetApartmentDraft(int id)
  {
    try
    {
      var result = _apartmentService.GetDraftApartments(id, UserHeader!.UserId);
      return Ok(result);
    }
    catch (Exception e) { return BadRequest(new UpdateResponseBase { Message = e.Message, Status = false, }); }
  }

  [HttpPost("update")] public IActionResult UpdateApartment(ApartmentRequest apartmentRequest)
  {
    try
    {
      var result = _apartmentService.UpdateApartment(apartmentRequest, UserHeader!.UserId);
      return Ok(result);
    }
    catch (Exception e) { return BadRequest(new UpdateResponseBase() { Message = e.Message, Status = false, }); }
  }

  [HttpGet("test")] public IActionResult GetTest()
  {
    try
    {
      return Ok(Timer.MergeTimeRanges(Timer.GetMostFrequentOverlappingTimeRanges(new List<TimeRange>
      {
        new TimeRange { Since = new DateTime(2023, 10, 13), To = new DateTime(2023, 10, 16), },
        new TimeRange { Since = new DateTime(2023, 10, 13), To = new DateTime(2023, 10, 16), },
        // new TimeRange { Since = new DateTime(2023, 10, 23), To = new DateTime(2023, 10, 27), },
        // new TimeRange { Since = new DateTime(2023, 10, 13), To = new DateTime(2023, 11, 24), },
        // new TimeRange { Since = new DateTime(2023, 10, 21), To = new DateTime(2023, 10, 29), },
        // new TimeRange { Since = new DateTime(2023, 11, 1), To = new DateTime(2023, 11, 24), },
      }, 2)));
    }
    catch (Exception e) { return BadRequest(new { error = "error", }); }
  }
}