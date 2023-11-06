using YGHMS.API.DTO.AdressDTOs;

namespace YGHMS.API.DTO.AccommodationDTOs
{
    public class CreateNewAccommodationDTO
    {
        public int Id { get; }//auto-increment
        public int OwnerId { get; set; }
        public int? EstateTypesId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Quality { get; set; }
        public string? Policies { get; set; }
        public DateTime? Expiration { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public AddressDTO Address { get; set; }
        public int? Status { get; set; }

    }
}
