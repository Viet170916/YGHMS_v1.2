namespace YGHMS.API.Infra.Models;

public partial class ReasonReport : CommonModel
{
  public int Id { get; set; }
  public int ReasonId { get; set; }
  public int ReportId { get; set; }
  public bool IsDeleted { get; set; }
  public virtual Reason Reason { get; set; } = null!;
  public virtual Report Report { get; set; } = null!;
}