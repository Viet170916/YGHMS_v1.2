namespace YGHMS.API.Infra.Models;

public partial class NotificationReview : CommonModel
{
  public int Id { get; set; }
  public int NotifycatonId { get; set; }
  public int ReviewId { get; set; }
  public bool IsDeleted { get; set; }
  public virtual Notification Notifycaton { get; set; } = null!;
  public virtual Review Review { get; set; } = null!;
}