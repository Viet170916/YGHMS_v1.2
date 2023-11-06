namespace YGHMS.API.Infra.Models;

public partial class NotificationFollow : CommonModel
{
  public int Id { get; set; }
  public int NotificationId { get; set; }
  public int FollowId { get; set; }
  public bool IsDeleted { get; set; }
  public virtual FollowUserAccom Follow { get; set; } = null!;
  public virtual Notification Notification { get; set; } = null!;
}