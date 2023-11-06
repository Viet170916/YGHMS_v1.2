namespace YGHMS.API.Infra.Models;

public partial class Accommodation : CommonModel
{
  public int Id { get; set; }
  public int OwnerId { get; set; }
  public int? EstateTypesId { get; set; }
  public string? Title { get; set; }
  public string? Description { get; set; }
  public decimal? Quality { get; set; }
  public int? AddressId { get; set; }
  public string? Policies { get; set; }
  public DateTime? Expiration { get; set; }
  public bool IsDeleted { get; set; }
  public double? Longitude { get; set; }
  public double? Latitude { get; set; }
  public int? Status { get; set; }
  public string? DetailAddress { get; set; }

  public virtual ICollection<AccommodationPublication> AccommodationPublications { get; set; } =
    new List<AccommodationPublication>();

  public virtual Address? Address { get; set; }
  public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
    public virtual Estatetype? EstateTypes { get; set; }
  public virtual ICollection<FollowUserAccom> FollowUserAccoms { get; set; } = new List<FollowUserAccom>();
  public virtual User Owner { get; set; } = null!;
  public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
  public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}