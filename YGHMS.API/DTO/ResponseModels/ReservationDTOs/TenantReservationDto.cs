using YGHMS.API.Infra.Enums;

namespace YGHMS.API.DTO.ResponseModels.ReservationDTOs;

public class TenantReservationDto
{
  public int? Id { get; set; }
  public UserResponse? Owner { get; set; }
  public UserResponse? Tenant { get; set; }
  public ReservationStatus Status { get; set; }
  public DateTime CreateAt { get; set; }
  public DateTime CheckinDate { get; set; }
  public DateTime CheckoutDate { get; set; }
  public AccommodationDetailRes Accommodation { get; set; } = null!;
}


