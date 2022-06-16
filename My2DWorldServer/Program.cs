using Microsoft.EntityFrameworkCore;
using My2DWorldShared;
using My2DWorldShared.DataEntities;
using My2DWorldServer.Calls;
using My2DWorldShared.Data;
using System.Text;
using System.Text.Json;
using My2DWorldShared.PacketsIn;
using My2DWorldServer.Services;
using NLog;
using NLog.Web;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
//test
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("Application init.");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.AddJsonFile("hostsettings.json", optional: true);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.Providers.Add<BrotliCompressionProvider>();
        options.Providers.Add<GzipCompressionProvider>();
    });

    builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
    {
        options.Level = CompressionLevel.Fastest;
    });

    builder.Services.Configure<GzipCompressionProviderOptions>(options =>
    {
        options.Level = CompressionLevel.Fastest;
    });

    builder.Services.AddSingleton<UsersSessionCollection>();
    builder.Services.AddScoped<UserSession>();
    builder.Services.AddScoped<IGameCaller, GameCaller>();
    builder.Services.AddScoped<IGameInformer, GameInformer>();
    builder.Services.AddDbContextFactory<SqlDbContext>();

    builder.Services.AddRazorPages();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseResponseCompression();

    app.UseHttpsRedirection();

    app.UseStaticFiles(new StaticFileOptions
    {
        ServeUnknownFileTypes = true
    });

    app.UseRouting();

    var webSocketOptions = new WebSocketOptions
    {
        KeepAliveInterval = TimeSpan.FromSeconds(15)
    };

    app.UseWebSockets(webSocketOptions);

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapRazorPages();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Application setup error.");
    throw;
}
finally
{
    LogManager.Shutdown();
}
