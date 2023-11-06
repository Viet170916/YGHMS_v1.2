namespace YGHMS.API.Infra.Models;

public partial class ApartmentPublication : CommonModel
{
  public int Id { get; set; }
  public int ApartmentId { get; set; }
  public int? MediaId { get; set; }
  public bool IsDeleted { get; set; }
  public virtual Apartment Apartment { get; set; } = null!;
  public virtual Publication? Media { get; set; }
}