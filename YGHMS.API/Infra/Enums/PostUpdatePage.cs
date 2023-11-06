using System.Diagnostics.CodeAnalysis;

namespace YGHMS.API.Infra.Enums;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum PostUpdatePage
{
  // ReSharper disable once InconsistentNaming
  ESTATE = 1,
  LOCATION = 2,
  POST_IMAGE = 3,
  TITLE_AND_DESC = 4,
  POLICIES = 5,
  APARTMENTS = 6,
  EXPIRATION = 7,
}