namespace YGHMS.API.DTO.AccommodationDTOs.ApartmentDTOs
{
    public class CreateNewApartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Quantity { get; set; }
        public int MaxOccupant { get; set; }
        public double? Price { get; set; }
        public float? Area { get; set; }
        public int? TypeOfBed { get; set; }
        public string? Description { get; set; }
        public int AccommodationId { get; set; }
        public int OwnerId { get; set; }
        public int? NumberOfBed { get; set; }
    }
}
