namespace YGHMS.API.DTO.ReservationDTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ApartmentId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Status { get; set; }
        public int OwnerId { get; set; }
        public int BedType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool isDeleted { get; set; }
    }
}
