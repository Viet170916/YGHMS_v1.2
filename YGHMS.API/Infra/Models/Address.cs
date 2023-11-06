namespace YGHMS.API.Infra.Models;

public partial class Address : CommonModel
{
  public int Id { get; set; }
  public string? Country { get; set; }
  public string? City { get; set; }
  public string? District { get; set; }
  public string? Commune { get; set; }
  public string? Detail { get; set; }
  public bool IsDeleted { get; set; }
  public double? Longitude { get; set; }
  public double? Latitude { get; set; }
  public string? Media { get; set; }
  public virtual ICollection<Accommodation> Accommodations { get; set; } = new List<Accommodation>();
  public virtual ICollection<Publication> MediaNavigation { get; set; } = new List<Publication>();
}