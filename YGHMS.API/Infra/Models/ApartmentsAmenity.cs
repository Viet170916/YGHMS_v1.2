namespace YGHMS.API.Infra.Models;

public partial class ApartmentsAmenity : CommonModel
{
  public int Id { get; set; }
  public int ApartmentId { get; set; }
  public int AmenityId { get; set; }
  public bool IsDeleted { get; set; }
  public virtual Amenity Amenity { get; set; } = null!;
  public virtual Apartment Apartment { get; set; } = null!;
}