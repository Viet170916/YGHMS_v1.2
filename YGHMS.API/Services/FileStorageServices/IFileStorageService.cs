using YGHMS.API.Common;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Infra.Models;

namespace YGHMS.API.Services.FileStorageServices;

public interface IFileStorageService
{
  public int SaveImageFile(
    IFormFile? file,
    PublicationType type = PublicationType.ACCOMMODATION,
    string? description = null
  );
}

public class FileStorageService : IFileStorageService
{
  private readonly IWebHostEnvironment _hostingEnvironment;
  private readonly RentalManagementContext _context;

  public FileStorageService(RentalManagementContext context, IWebHostEnvironment hostingEnvironment)
  {
    _context = context;
    _hostingEnvironment = hostingEnvironment;
  }

  public int SaveImageFile(IFormFile? file, PublicationType type, string? description = null)
  {
    if (file == null || file.Length == 0) { throw new(Constants.ErrorMessages.BadParam); }

    var webRootPath = _hostingEnvironment.WebRootPath;
    var folderPath = Path.Combine(webRootPath, "images");
    Directory.CreateDirectory(folderPath);
    var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(file.FileName);
    var filePath = Path.Combine(folderPath, fileName);
    using (var stream = new FileStream(filePath, FileMode.Create)) { file.CopyTo(stream); }

    var image = new Publication()
    {
      Url = Path.Combine("images", fileName),
      Description = description,
      MediaType = (int)type,
      ModifyAt = DateTime.Now,
      CreateAt = DateTime.Now,
      IsDeleted = 0,
    };
    _context.Publications.Add(image);
    _context.SaveChanges();
    return image.Id;
  }
}