namespace YGHMS.API.Infra.Models;

public partial class ApartmentBenefit : CommonModel
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public string? Content { get; set; }
  public int Type { get; set; }
  public int ApartmentId { get; set; }
  public sbyte IsDelete { get; set; }
  public virtual Apartment Apartment { get; set; } = null!;
}