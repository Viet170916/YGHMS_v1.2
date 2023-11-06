namespace YGHMS.API.Infra.Models;

public partial class ApartmentBedType : CommonModel
{
  public int Id { get; set; }
  public int ApartmentId { get; set; }
  public int Type { get; set; }
  public int Quantity { get; set; }
  public double Price { get; set; }
  public DateTime CreateAt { get; set; }
  public DateTime ModifyAt { get; set; }
  public sbyte IsDelete { get; set; }
  public virtual Apartment Apartment { get; set; } = null!;

  public virtual ICollection<Unavailableapartmentdate> Unavailableapartmentdates { get; set; } =
    new List<Unavailableapartmentdate>();
}