namespace YGHMS.API.Infra.Models;

public partial class Estatetype : CommonModel
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public bool IsDeleted { get; set; }
  public virtual ICollection<Accommodation> Accommodations { get; set; } = new List<Accommodation>();
}