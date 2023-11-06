namespace YGHMS.API.DTO.FollowDTOs
{
    public class FollowDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccomodationId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
