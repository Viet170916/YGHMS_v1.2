namespace YGHMS.API.Infra.Models;

public partial class Reservation : CommonModel
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public int ApartmentId { get; set; }
  public DateTime FromDate { get; set; }
  public DateTime ToDate { get; set; }
  public int Status { get; set; }
  public bool IsDeleted { get; set; }
  public int OwnerId { get; set; }
  public int BedType { get; set; }
  public virtual Apartment Apartment { get; set; } = null!;

  public virtual ICollection<NotificationReservation> NotificationReservations { get; set; } =
    new List<NotificationReservation>();

  public virtual User Owner { get; set; } = null!;
  public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
  public virtual User User { get; set; } = null!;
}