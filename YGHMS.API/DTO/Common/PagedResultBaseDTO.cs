namespace YGHMS.API.DTO.Common;

public abstract class PagedResultBaseDTO
{
  public int CurrentPage { get; set; }
  public int PageCount { get; set; }
  public int PageSize { get; set; }
  public int RowCount { get; set; }
  public int MaxSetting { get; set; }
  public int FirstRowOnPage => Math.Min(RowCount, (CurrentPage - 1) * PageSize + 1);
  public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
}