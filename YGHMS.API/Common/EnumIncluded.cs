namespace YGHMS.API.Common;

public static class EnumIncluded
{
  public static bool IsIncluded<TEnum>(TEnum valueToCheck)
  {
    var enumValues = new HashSet<TEnum>((TEnum[])Enum.GetValues(typeof(TEnum)));
    return enumValues.Contains(valueToCheck);
  }
}