namespace YGHMS.API.Common;

public static class QueryableExtension
{
  public static IEnumerable<T> Paging<T>(this IEnumerable<T> query, int page, int take)
  {
    var random = new Random();
    return query
           // .OrderBy((acon) => random.Next())
           .Skip(take * (page - 1))
           .Take(take);
  }

  public static IQueryable<T> Paging<T>(this IQueryable<T> query, int page, int take)
  {
    return query.Skip(take * (page - 1))
                .Take(take);
  }
}