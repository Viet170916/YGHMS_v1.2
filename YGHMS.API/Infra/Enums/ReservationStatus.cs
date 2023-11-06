// ReSharper disable InconsistentNaming
namespace YGHMS.API.Infra.Enums;

public enum ReservationStatus
{
  PENDING = 0,
  PAYMENT_WAITING = 1,
  CANCELED = 2,
  PAID = 3,
  DONE = 4,
  REJECTED = 5,
  DRAFT = 6,
}