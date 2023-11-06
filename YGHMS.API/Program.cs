using Microsoft.AspNetCore.Builder;
using YGHMS.API;
using Serilog;
using YGHMS.API;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.ConfigureServices();
var app = builder.Build().ConfigurePipeline();
Log.Logger.Error("Start API");
app.Run();