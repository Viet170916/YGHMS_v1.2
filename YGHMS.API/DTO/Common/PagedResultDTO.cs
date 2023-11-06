namespace YGHMS.API.DTO.Common;

public class PagedResultDTO<T> : PagedResultBaseDTO where T : class
{
  public List<T> Results { get; set; }

  public PagedResultDTO(int currentPage, int pageSize, int rowCount, int maxSetting = 0)
  {
    CurrentPage = currentPage;
    PageSize = pageSize;
    RowCount = rowCount;
    PageCount = (int)Math.Ceiling((double)rowCount / pageSize);
    MaxSetting = maxSetting;
  }

  public PagedResultDTO(int currentPage, int pageSize, int rowCount, List<T> results, int maxSetting = 0)
  {
    CurrentPage = currentPage;
    PageSize = pageSize;
    RowCount = rowCount;
    PageCount = (int)Math.Ceiling((double)rowCount / pageSize);
    Results = results;
    MaxSetting = maxSetting;
  }

  public PagedResultDTO() { Results = new List<T>(); }
}