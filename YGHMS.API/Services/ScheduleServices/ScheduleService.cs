using Microsoft.EntityFrameworkCore;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Enums;

namespace YGHMS.API.Services.ScheduleServices;

public class ScheduleService : IHostedService
{
  private readonly Timer _timer;
  private readonly IServiceScopeFactory _scopeFactory;

  public ScheduleService(IServiceScopeFactory scopeFactory)
  {
    _scopeFactory = scopeFactory;
    _timer = new Timer(UpdateStatus, null, 0, 1000 * 60 * 60 * 24);
  }

  private void UpdateStatus(object? status)
  {
    using (var scope = _scopeFactory.CreateScope())
    {
      var context = scope.ServiceProvider.GetRequiredService<RentalManagementContext>();
      var posts = context.Accommodations
                         .Include(acc => acc.Owner)
                         .Where(post =>
                           post.Expiration < DateTime.Now && post.Owner.Balance >= 20000 &&
                           post.Status == (int)PostStatus.ACTIVATED);
      if (posts.Any())
      {
        foreach (var accommodation in posts)
        {
          accommodation.Status = (int)PostStatus.EXPIRED;
          accommodation.ModifyAt = DateTime.Now;
          accommodation.Owner.Balance -= 20000;
        }

        context.SaveChanges();
      }
    }
  }

  public void Dispose() { _timer?.Dispose(); }
  public Task StartAsync(CancellationToken cancellationToken) { return Task.CompletedTask; }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    _timer?.Change(Timeout.Infinite, 0);
    return Task.CompletedTask;
  }
}