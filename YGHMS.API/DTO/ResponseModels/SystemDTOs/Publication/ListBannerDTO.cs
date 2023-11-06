namespace YGHMS.API.DTO.ResponseModels.SystemDTOs.Publication;

public class ListBannerDto
{
  public List<BannerDto> Banner =>
    new()
    {
      new BannerDto
      {
        FirstTitle = "", LastTitle = "", Location = "", ImageUrl = "",
      },
      new BannerDto
      {
        FirstTitle = "", LastTitle = "", Location = "", ImageUrl = "",
      },
      new BannerDto
      {
        FirstTitle = "", LastTitle = "", Location = "", ImageUrl = "",
      },
      new BannerDto
      {
        FirstTitle = "", LastTitle = "", Location = "", ImageUrl = "",
      },
    };
}