using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YGHMS.API.Common;
using YGHMS.API.DTO.Common;
using YGHMS.API.DTO.ResponseModels;
using YGHMS.API.DTO.ResponseModels.AddressDTOs;
using YGHMS.API.Services.AddressServices;

namespace YGHMS.API.Controllers.LocationController;

[Route("api/location")]
[ApiController]
public class AddressController : ControllerBase
{
  private readonly ILogger<AddressController> _logger;
  private readonly IAddressService _addressService;

  public AddressController(ILogger<AddressController> logger, IAddressService addressService)
  {
    _logger = logger;
    _addressService = addressService;
  }

  [HttpGet("search")] public ResponseDTO<List<AddressDto>> GetAddressesList(string q)
  {
    return _addressService.GetFirstFiveAddressesBySearching(q);
  }

  [HttpGet("search-exact")] public IActionResult GetExactAddresses(string q)
  {
    try { return Ok(_addressService.GetExactLocation(q)); }
    catch (Exception e)
    {
      _logger.LogError(e.Message);
      return NotFound(new UpdateResponseBase { Message = Constants.ErrorMessages.NotFound, Status = false, });
    }
  }
}