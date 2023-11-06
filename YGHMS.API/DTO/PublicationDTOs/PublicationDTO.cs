namespace YGHMS.API.DTO.PublicationDTOs;

public class PublicationDTO
{
  public int Id { get; }
  public string Url { get; set; } = null!;
  public string? Description { get; set; }
  public int MediaType { get; set; }
}