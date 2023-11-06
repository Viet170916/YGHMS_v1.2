namespace YGHMS.API.Infra.Models;

public partial class Publication : CommonModel
{
  public int Id { get; set; }
  public string Url { get; set; } = null!;
  public string? Description { get; set; }
  public sbyte IsDeleted { get; set; }
  public int MediaType { get; set; }

  public virtual ICollection<AccommodationPublication> AccommodationPublications { get; set; } =
    new List<AccommodationPublication>();

  public virtual ICollection<ApartmentPublication> ApartmentPublications { get; set; } =
    new List<ApartmentPublication>();

  public virtual ICollection<User> UserAvatars { get; set; } = new List<User>();
  public virtual ICollection<User> UserCoverImageNavigations { get; set; } = new List<User>();
  public virtual ICollection<UserHighlight> UserHighlights { get; set; } = new List<UserHighlight>();
  public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}