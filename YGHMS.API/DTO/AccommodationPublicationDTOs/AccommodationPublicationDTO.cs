namespace YGHMS.API.DTO.AccommodationPublicationDTOs;

public class AccommodationPublicationDTO
{
  public int Id { get; } //auto-increment
  public int MediaId { get; set; } //fk detailMedium
  public int AccommodationId { get; set; } //fk Accommodation
}