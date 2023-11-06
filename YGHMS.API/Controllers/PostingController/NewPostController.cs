using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YGHMS.API.Common;
using YGHMS.API.DTO.AccommodationDTOs.ApartmentDTOs;
using YGHMS.API.DTO.Common;
using YGHMS.API.DTO.PublicationDTOs;
using YGHMS.API.DTO.RequestModels;
using YGHMS.API.DTO.ResponseModels;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Infra.Models;
using YGHMS.API.Services.AccommodationServices;
using YGHMS.API.Services.ApartmentServices;
using YGHMS.API.Services.MediaServices;

namespace YGHMS.API.Controllers.PostingController;

[Route("api/post/new")]
[ApiController]
public class NewPostController : BaseApiController
{
  private readonly ILogger<NewPostController> _logger;
  private readonly IAccommodationService _accommodationService;
  private readonly IApartmentService _apartmentService;
  private readonly IDetailMediaService _detailMediaService;
  private readonly IAccommodationPublicationService _accommodationMediaService;
  private readonly IApartmentPublicationService _apartmentMediaService;
  private readonly IMapper _mapper;

  public NewPostController(
    ILogger<NewPostController> logger,
    IAccommodationService accommodationService,
    IDetailMediaService detailMediaService,
    IAccommodationPublicationService accommodationMediaService,
    IApartmentService apartmentService,
    IApartmentPublicationService apartmentMediaService,

    IMapper mapper
  )
  {
    _logger = logger;
    _accommodationService = accommodationService;
    _detailMediaService = detailMediaService;
    _accommodationMediaService = accommodationMediaService;
    _apartmentService = apartmentService;
    _apartmentMediaService = apartmentMediaService;

    _mapper = mapper;
  }

  [HttpPost("new-id")] public IActionResult PostNewAccommodation()
  {
    try
    {
      var newAccommodationId = _accommodationService.AddAccommodation(UserHeader!.UserId);
      if (newAccommodationId is null)
      {
        return NotFound(new UpdateResponseBase { Status = false, Message = Constants.ErrorMessages.NotFound, });
      }

      return Ok(new { PostId = newAccommodationId, Page = 1, });
    }
    catch (Exception e)
    {
      _logger.LogError(e.Message);
      return NotFound(new UpdateResponseBase { Status = false, Message = Constants.ErrorMessages.NotFound, });
    }
  }

    
  public class UpdatePostAccommodationBody
  {
    public int Page { get; set; }
    public AccommodationRequest? Accommodation { get; set; }
  }

  [HttpPost("update")] public IActionResult UpdatePostAccommodation(UpdatePostAccommodationBody? body)
  {
    try
    {
      Accommodation apartment = _mapper.Map<Accommodation>(body.Accommodation);

      var nextPage =
        _accommodationService.UpdateDraftAccommodation(UserHeader!.UserId, apartment!,
          body.Page);
      return Ok(new { PostId = body.Accommodation!.Id, Page = nextPage, });
    }
    catch (Exception e)
    {
      _logger.LogError(e.Message);
      return NotFound(new UpdateResponseBase { Status = false, Message = e.Message, });
    }
  }
    

    

  [HttpPost("update/apartment")]
  public IActionResult UpdateAddApartmentsAccommodation(int page, Accommodation accommodation)
  {
    try
    {
      if (page is (int)PostUpdatePage.POST_IMAGE or (int)PostUpdatePage.APARTMENTS) return Forbid();
      var nextPage = _accommodationService.UpdateDraftAccommodation(UserHeader!.UserId, accommodation, page);
      return Ok(new { PostId = accommodation.Id, Page = nextPage, });
    }
    catch (Exception e)
    {
      _logger.LogError(e.Message);
      return NotFound(new UpdateResponseBase { Status = false, Message = Constants.ErrorMessages.NotFound, });
    }
  }

  [HttpPost("/accommodation_publications")]
  public IActionResult PostDetailMediumOfAccommodation(int accommodationID, List<PublicationDTO> publicationDTOs)
  {
    try
    {
      var listOfPublicationIDsResponse = _detailMediaService.AddListOfDetailMedium(publicationDTOs);
      if (listOfPublicationIDsResponse.Code == (int)RESPONSE_CODE.Created)
      {
        return Ok(_accommodationMediaService.AddListMediaOfAccommodation(accommodationID,
          listOfPublicationIDsResponse.Data));
      }
      else { return BadRequest(); }
    }
    catch (Exception ex) { return BadRequest(ex); }
  }

  [HttpPost("/apartments")] public IActionResult PostNewApartments(CreateNewApartmentDTO[] newApartmentDTOs)
  {
    try { return Ok(_apartmentService.AddListOfApartment(newApartmentDTOs).Data); }
    catch (Exception ex) { return BadRequest(ex); }
  }

  [HttpPost("/apartment_publications")]
  public IActionResult PostDetailMediumOfApartment(int apartmentID, List<PublicationDTO> publicationDTOs)
  {
    try
    {
      var listOfPublicationIDsResponse = _detailMediaService.AddListOfDetailMedium(publicationDTOs);
      if (listOfPublicationIDsResponse.Code == (int)RESPONSE_CODE.Created)
      {
        return Ok(_apartmentMediaService.AddListMediaOfApartment(apartmentID, listOfPublicationIDsResponse.Data));
      }
      else { return BadRequest(); }
    }
    catch (Exception ex) { return BadRequest(ex); }
  }
}