namespace YGHMS.API.DTO.RequestModels;

public class BookingRequest
{
  public int? Id{ get; set; }
  public DateTime? CreateAt { get; set; }
  public int ApartmentId { get; set; }
  public DateTime? FromDate { get; set; }
  public DateTime? ToDate { get; set; }
  public int? BedType { get; set; }
  public UserRequestForBooking? User { get; set; }
}
public class UserRequestForBooking{
  public int? Id { get; set; }
  public string? FullName { get; set; }
  public string? PhoneNumber { get; set; }

}