namespace YGHMS.API.Infra.Models;

public partial class User : CommonModel
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public int? AvatarId { get; set; }
    public string? UserName { get; set; }
    public double Balance { get; set; }
    public int TransactionStatus { get; set; }
    public int RoleId { get; set; }
    public int Status { get; set; }
    public int? CoverImage { get; set; }
    public virtual ICollection<Accommodation> Accommodations { get; set; } = new List<Accommodation>();
    public virtual Account? Account { get; set; }
    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
    public virtual Publication? Avatar { get; set; }
    public virtual ICollection<Chat> ChatReceiveds { get; set; } = new List<Chat>();
    public virtual ICollection<Chat> ChatSendeds { get; set; } = new List<Chat>();
    public virtual Publication? CoverImageNavigation { get; set; }
    public virtual ICollection<FollowUserAccom> FollowUserAccoms { get; set; } = new List<FollowUserAccom>();
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
    public virtual ICollection<Reservation> ReservationOwners { get; set; } = new List<Reservation>();
    public virtual ICollection<Reservation> ReservationUsers { get; set; } = new List<Reservation>();
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual Role Role { get; set; } = null!;
    public virtual ICollection<UserHighlight> UserHighlights { get; set; } = new List<UserHighlight>();
}