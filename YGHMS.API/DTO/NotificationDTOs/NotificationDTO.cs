using YGHMS.API.DTO.FollowDTOs;
using YGHMS.API.DTO.ReservationDTOs;
using YGHMS.API.DTO.ReviewDTOs;

namespace YGHMS.API.DTO.NotificationDTOs
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Author { get; set; }
        public bool IsRead { get; set; }
        public int Type { get; set; }
        public ReviewResponseDTO? Review { get; set; }
        public ReservationDTO? Reservation { get; set; }
        public FollowDTO? Follow { get; set; }
    }
}
