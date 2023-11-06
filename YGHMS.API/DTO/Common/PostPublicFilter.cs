using YGHMS.API.DTO.ResponseModels.PostDTOs.FilterResponse;

namespace YGHMS.API.DTO.Common;

public class PostPublicFilter
{
  public int Page { get; set; } = 1;
  public HomeFilterResponse? Filter { get; set; }
}