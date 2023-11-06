using System.Diagnostics.CodeAnalysis;

namespace YGHMS.API.Infra.Enums;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum AmenityType
{
  ROOM = 0,
  FURNITURE = 1,
  FACILITY = 2,
  ROOM_OFFER = 3,
  PAYMENT = 4,
  DISTANCE_TO_CENTER = 5,
  BEACH_ACCESS = 6,
  SAFETY = 7,
  POLICY = 8,
}