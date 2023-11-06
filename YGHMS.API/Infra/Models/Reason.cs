namespace YGHMS.API.Infra.Models;

public partial class Reason : CommonModel
{
  public int Id { get; set; }
  public string Content { get; set; } = null!;
  public bool IsDeleted { get; set; }
  public virtual ICollection<ReasonReport> ReasonReports { get; set; } = new List<ReasonReport>();
}