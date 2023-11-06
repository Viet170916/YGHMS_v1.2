namespace YGHMS.API.Infra.Models;

public partial class NotificationReservation : CommonModel
{
  public int Id { get; set; }
  public int NotificationId { get; set; }
  public int ReservationId { get; set; }
  public bool IsDeleted { get; set; }

  public virtual Notification Notification { get; set; } = null!;
  public virtual Reservation Reservation { get; set; } = null!;
}