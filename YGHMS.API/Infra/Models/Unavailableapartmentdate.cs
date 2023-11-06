namespace YGHMS.API.Infra.Models;

public partial class Unavailableapartmentdate : CommonModel
{
  public int Id { get; set; }
  public int ApartmentTypeId { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public bool IsDeleted { get; set; }
  public virtual ApartmentBedType ApartmentType { get; set; } = null!;
}