using Microsoft.AspNetCore.Mvc;
using YGHMS.API.Common;
using YGHMS.API.DTO.ResponseModels;
using YGHMS.API.Services.FileStorageServices;

namespace YGHMS.API.Controllers.PublicationController;

[Route("/api/publication")]
[ApiController]
public class PublicationController : BaseApiController
{
  private readonly ILogger<PublicationController> _logger;
  private readonly IFileStorageService _fileStorageService;

  public PublicationController(IFileStorageService fileStorageService, ILogger<PublicationController> logger)
  {
    _fileStorageService = fileStorageService;
    _logger = logger;
  }

  [HttpPost("add-images")] public IActionResult UpdateImagePostAccommodation([FromForm] List<IFormFile> file)
  {
    try
    {
      IList<int> fileIds = new List<int>();
      foreach (var formFile in file) { fileIds.Add(_fileStorageService.SaveImageFile(formFile)); }

      return Ok(fileIds);
    }
    catch (Exception e)
    {
      _logger.LogError(e.Message);
      return NotFound(new UpdateResponseBase { Status = false, Message = Constants.ErrorMessages.NotFound, });
    }
  }
}