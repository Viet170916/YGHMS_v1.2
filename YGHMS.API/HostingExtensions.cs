using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Config;
using YGHMS.API.Services.AccommodationServices;
using YGHMS.API.Services.AccountServices;
using YGHMS.API.Services.AddressServices;
using YGHMS.API.Services.ApartmentServices;
using YGHMS.API.Services.AuthenServices;
using YGHMS.API.Services.FileStorageServices;
using YGHMS.API.Services.FollowServices;
using YGHMS.API.Services.MediaServices;
using YGHMS.API.Services.NotiServices;
using YGHMS.API.Services.PostServices;
using YGHMS.API.Services.ReservationServices;
using YGHMS.API.Services.ReviewServices;
using YGHMS.API.Services.ScheduleServices;

namespace YGHMS.API;

public static class HostingExtensions
{
  private const string MyAllowSpecificOrigins = "AllowOrigin";

  public static void ConfigureServices(this WebApplicationBuilder builder)
  {
    builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
    {
      AppSettings.Instance.SetConfiguration(hostingContext.Configuration);
    });
    builder.Services.AddControllers();
    //Learn more about configuring Swagger / OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddHttpClient();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHealthChecks();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddScoped<IAddressService, AddressService>();
    builder.Services.AddScoped<IReservationService, ReservationService>();
    builder.Services.AddScoped<IApartmentService, ApartmentService>();
    builder.Services.AddScoped<IPostService, PostService>();
    builder.Services.AddScoped<IAuthenService, AuthenService>();
    builder.Services.AddScoped<IProfileService, ProfileService>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<IFollowService, FollowService>();
    builder.Services.AddScoped<IReviewService, ReviewService>();
    builder.Services.AddScoped<INotiService, NotiService>();
    builder.Services.AddScoped<IFileStorageService, FileStorageService>();
    builder.Services.AddScoped<IAccommodationService, AccommodationService>();
    builder.Services.AddScoped<IDetailMediaService, DetailMediaService>();
    builder.Services.AddScoped<IAccommodationPublicationService, AccommodationMediaService>();
    builder.Services.AddScoped<IApartmentPublicationService, ApartmentMediaService>();
    var str = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<RentalManagementContext>(options => { options.UseMySQL(str); });
    var logger = new LoggerConfiguration().ReadFrom
                                          .Configuration(builder.Configuration)
                                          .Enrich
                                          .FromLogContext()
                                          .CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);
    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
      options.DefaultRequestCulture = new RequestCulture("en-US");
    });
    builder.Services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
    {
      builder.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader();
    }));
    builder.Services.AddHostedService<ScheduleService>();
  }

  public static WebApplication ConfigurePipeline(this WebApplication app)
  {
    // Configure the HTTP request pipeline.
    InitializeDatabase(app);
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseRouting();
    app.MapControllers();
    app.UseStaticFiles();
    app.UseCors(MyAllowSpecificOrigins);
    app.UseEndpoints(endpoints =>
    {
      endpoints.MapControllers();
      endpoints.MapHealthChecks("/healthy");
      endpoints.MapGet("/", async context => { await context.Response.WriteAsync("[API] Rental Management System"); });
    });
    app.UseStaticFiles();
    app.UseRequestLocalization();
    app.Run();
    return app;
  }

  private static async void InitializeDatabase(IApplicationBuilder app)
  {
    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    {
      var utcNow = DateTime.UtcNow;
      var context = serviceScope.ServiceProvider.GetService<RentalManagementContext>();
      await context.Database.MigrateAsync();
    }
  }
}