namespace YGHMS.API.Infra.Models;

public partial class Review : CommonModel
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public int ReservationId { get; set; }
  public decimal Rate { get; set; }
  public string? Comment { get; set; }
  public bool IsDeleted { get; set; }
  public virtual ICollection<NotificationReview> NotificationReviews { get; set; } = new List<NotificationReview>();
  public virtual Reservation Reservation { get; set; } = null!;
  public virtual User User { get; set; } = null!;
}