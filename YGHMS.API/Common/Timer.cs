using YGHMS.API.DTO.ResponseModels.PostDTOs.FilterResponse;

namespace YGHMS.API.Common;

public static class Timer
{
  public static List<TimeRange> MergeTimeRanges(List<TimeRange>? timeRanges)
  {
    if (timeRanges == null || timeRanges.Count == 0) return new List<TimeRange>();
    var sortedTimeRanges = timeRanges.OrderBy(range => range.Since)
                                     .ThenBy(range => range.To)
                                     .ToList();
    var mergedTimeRanges = new List<TimeRange> { sortedTimeRanges[0], };
    foreach (var range in sortedTimeRanges.Skip(1))
    {
      var lastMerged = mergedTimeRanges.Last();
      if (range.Since <= lastMerged.To)
      {
        if (range.To > lastMerged.To) lastMerged.To = range.To;
      }
      else
        mergedTimeRanges.Add(range);
    }

    return mergedTimeRanges;
  }

  private static bool IsOverlap(TimeRange range1, TimeRange range2)
  {
    return range1.Since < range2.To && range2.Since < range1.To;
  }

  public static TimeRange? GetFrequentOverlappingTimeRanges(List<TimeRange>? intervals)
  {
    
    if (AllDateRangesAreEqual(intervals)) return intervals.FirstOrDefault();
    var dateCount = new Dictionary<DateTime?, int>();
    var overlappingRanges = new List<TimeRange>();
    foreach (var interval in intervals)
    {
      var start = interval.Since;
      var end = interval.To;
      while (start <= end)
      {
        if (dateCount.ContainsKey(start))
          dateCount[start]++;
        else
          dateCount[start] = 1;
        start = start?.AddDays(1);
      }
    }

    DateTime? startDate = null;
    DateTime? endDate = null;
    foreach (var entry in dateCount)
      if (entry.Value >= intervals.Count)
      {
        if (startDate == null) startDate = entry.Key;
        endDate = entry.Key;
      }
      else
        if (startDate != null)
        {
          if (endDate != null) overlappingRanges
            .Add(new TimeRange { Since = startDate.Value, To = endDate.Value, });
          startDate = null;
          endDate = null;
        }

    return overlappingRanges.Count > 0 ? overlappingRanges[0] : null;
  }

  public static List<TimeRange> GetMostFrequentOverlappingTimeRanges(List<TimeRange> intervals, int count)
  {
    var sortedTimeRanges = intervals.OrderBy(range => range.Since)
                                    .ThenBy(range => range.To)
                                    .ToList();
    List<TimeRange>? timeRanges = new();
    for (var i = 0; i <= (intervals?.Count ?? 1) - count; i++)
    {
      var range = GetFrequentOverlappingTimeRanges(intervals?.Skip(i).Take(count).ToList());
      if (range != null) timeRanges.Add(range);
    }

    return timeRanges;
  }

  private static bool AllDateRangesAreEqual(List<TimeRange> dateRanges)
  {
    if (dateRanges.Count < 2) return true;
    var
      firstStartDate = dateRanges[0].Since?.Date;
    foreach (var timeRange in dateRanges)
    {
      var startDate = timeRange.Since?.Date;
      var endDate = timeRange.To?.Date;
      if (startDate != firstStartDate || endDate != firstStartDate) return false;
    }

    return true;
  }
}