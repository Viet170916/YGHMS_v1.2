namespace YGHMS.API.Infra.Models;

public partial class Notification : CommonModel
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public sbyte IsRead { get; set; }
  public int Type { get; set; }
  public bool IsDeleted { get; set; }
  public virtual ICollection<NotificationReservation> NotificationReservations { get; set; } = new List<NotificationReservation>();
  public virtual ICollection<NotificationFollow> NotificationFollows { get; set; } = new List<NotificationFollow>();
  public virtual ICollection<NotificationReview> NotificationReviews { get; set; } = new List<NotificationReview>();
}