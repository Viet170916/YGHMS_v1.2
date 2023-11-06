using YGHMS.API.DTO.ResponseModels.ApartmentDTOs;

namespace YGHMS.API.DTO.AccommodationDTOs;

public class GetListRequestAccommodationDTO
{
  public int Id { get; set; }
  public string Title { get; set; }
  public GetListRequestApartmentDTO Apartment { get; set; }
}