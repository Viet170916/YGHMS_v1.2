using YGHMS.API.DTO.ResponseModels.ApartmentDTOs;
using YGHMS.API.Infra.Enums;

namespace YGHMS.API.DTO.ResponseModels.ReservationDTOs;

public class UserResponse
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public string? AvatarUrl { get; set; }
  public string? Email { get; set; }
  public string? Phone { get; set; }
}

public class AccommodationResponse
{
  public int Id { get; set; }
  public string? Title { get; set; }
  public ApartmentResponse Apartment { get; set; } = null!;
}

public class LandlordReservationDto
{
  public int Id { get; set; }
  public DateTime CreateAt { get; set; }
  public DateTime CheckinDate { get; set; }
  public DateTime CheckoutDate { get; set; }
  public ReservationStatus Status { get; set; }
  public UserResponse Tenant { get; set; } = null!;
  public AccommodationResponse Accommodation { get; set; } = null!;
}

public class ReservationDetailRes
{
  public int Id { get; set; }
  public DateTime CreateAt { get; set; }
  public DateTime CheckinDate { get; set; }
  public DateTime CheckoutDate { get; set; }
  public int Status { get; set; }
  public UserResponse Tenant { get; set; } = null!;
  public AccommodationDetailRes Accommodation { get; set; } = null!;
}

public class AccommodationDetailRes
{
  public int Id { get; set; }
  public string? Title { get; set; } = null!;
  public IList<string> Images { get; set; } = null!;
  public ApartmentDetailRes Apartment { get; set; } = null!;
}

public class ApartmentDetailRes
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public string ThumbnailUrl { get; set; } = null!;
  public int? TypeOfBed { get; set; }
  public double? Price { get; set; }
}