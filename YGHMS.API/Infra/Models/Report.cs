namespace YGHMS.API.Infra.Models;

public partial class Report : CommonModel
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public int AccomodationId { get; set; }
  public bool IsDeleted { get; set; }
  public virtual Accommodation Accomodation { get; set; } = null!;
  public virtual ICollection<ReasonReport> ReasonReports { get; set; } = new List<ReasonReport>();
  public virtual User User { get; set; } = null!;
}