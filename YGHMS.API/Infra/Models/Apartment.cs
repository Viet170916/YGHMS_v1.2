namespace YGHMS.API.Infra.Models;

public partial class Apartment : CommonModel
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public int? Quantity { get; set; }
  public int MaxOccupant { get; set; }
  public double? Price { get; set; }
  public float? Area { get; set; }
  public int? TypeOfBed { get; set; }
  public string? Description { get; set; }
  public bool IsDeleted { get; set; }
  public int AccommodationId { get; set; }
  public int OwnerId { get; set; }
  public int? NumberOfBed { get; set; }
  public virtual Accommodation Accommodation { get; set; } = null!;
  public virtual ICollection<ApartmentBedType> ApartmentBedTypes { get; set; } = new List<ApartmentBedType>();
  public virtual ICollection<ApartmentBenefit> ApartmentBenefits { get; set; } = new List<ApartmentBenefit>();

  public virtual ICollection<ApartmentPublication> ApartmentPublications { get; set; } =
    new List<ApartmentPublication>();

  public virtual ICollection<ApartmentsAmenity> ApartmentsAmenities { get; set; } = new List<ApartmentsAmenity>();
  public virtual User Owner { get; set; } = null!;
  public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}