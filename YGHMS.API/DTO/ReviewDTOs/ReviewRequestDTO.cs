namespace YGHMS.API.DTO.ReviewDTOs
{
    public class ReviewRequestDTO
    {
        public int UserId { get; set; }
        public int ReservationId { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
    }
    public class ReviewResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ReservationId { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public DateTime CreateAt { get;set; }
        public DateTime ModifiAt { get; set; }
    }
}
