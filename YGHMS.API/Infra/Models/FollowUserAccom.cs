namespace YGHMS.API.Infra.Models;

public partial class FollowUserAccom : CommonModel
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public int AccomodationId { get; set; }
  public bool IsDeleted { get; set; }
  public virtual Accommodation Accomodation { get; set; } = null!;
  public virtual ICollection<NotificationFollow> NotificationFollows { get; set; } = new List<NotificationFollow>();
  public virtual User User { get; set; } = null!;
}