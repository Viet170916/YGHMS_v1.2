namespace YGHMS.API.Infra.Models;

public partial class Amenity : CommonModel
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public int? Type { get; set; }
  public string? Description { get; set; }
  public bool IsDeleted { get; set; }
  public DateTime CreateAt { get; set; }
  public DateTime ModifyAt { get; set; }
  public virtual ICollection<ApartmentsAmenity> ApartmentsAmenities { get; set; } = new List<ApartmentsAmenity>();
}