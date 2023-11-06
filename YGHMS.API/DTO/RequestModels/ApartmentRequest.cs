namespace YGHMS.API.DTO.RequestModels;

public class ApartmentRequest
{
  public int? Id { get; set; }
  public string? Name { get; set; } = null!;
  public IList<Image>? Images { get; set; } = null!;
  
  public int? Quantity { get; set; }
  public int? MaxOccupant { get; set; }
  public double? Price { get; set; }
  public float? Area { get; set; }
  public string? Description { get; set; }
  public int? AccommodationId { get; set; }
  public ICollection<ApartmentBedTypeRequest>? ApartmentBedTypes { get; set; } = new List<ApartmentBedTypeRequest>();

  public ICollection<ApartmentPublicationRequest>? ApartmentPublications { get; set; } =
    new List<ApartmentPublicationRequest>();

  public ICollection<ApartmentsAmenityRequest>? ApartmentsAmenities { get; set; } =
    new List<ApartmentsAmenityRequest>();
}

public class ApartmentBedTypeRequest
{
  public int? Id { get; set; }
  public int? ApartmentId { get; set; }
  public int? Type { get; set; }
  public int? Quantity { get; set; }
  public double? Price { get; set; }
}

public class ApartmentsAmenityRequest
{
  public int? Id { get; set; }
  public int? ApartmentId { get; set; }
  public int? AmenityId { get; set; }
}

public class ApartmentPublicationRequest
{
  public int? Id { get; set; }
  public int? ApartmentId { get; set; }
  public int? MediaId { get; set; }
}

public class Image
{
  public string? Url { get; set; }
  public string? Description { get; set; }
}