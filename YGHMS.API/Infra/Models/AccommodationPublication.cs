namespace YGHMS.API.Infra.Models;

public partial class AccommodationPublication : CommonModel
{
  public int Id { get; set; }
  public int? MediaId { get; set; }
  public int AccommodationId { get; set; }
  public bool IsDeleted { get; set; }
  public virtual Accommodation Accommodation { get; set; } = null!;
  public virtual Publication? Media { get; set; }
}