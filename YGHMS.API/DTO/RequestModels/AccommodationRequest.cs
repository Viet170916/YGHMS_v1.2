namespace YGHMS.API.DTO.RequestModels;

public class AccommodationRequest
{
  public int Id { get; set; }
  public int? EstateTypesId { get; set; }
  public string? Title { get; set; }
  public string? Description { get; set; }
  public decimal? Quality { get; set; }
  public int? AddressId { get; set; }
  public string? Policies { get; set; }
  public DateTime? Expiration { get; set; }
  public double? Longitude { get; set; }
  public double? Latitude { get; set; }
  public string? DetailAddress { get; set; }
  public IList<AccommodationPublicationRequest>? AccommodationPublications { get; set; }
}

public class AccommodationPublicationRequest
{
  public int? Id { get; set; }
  public int? MediaId { get; set; }
  public string? Url { get; set; }
  public int AccommodationId { get; set; }
}