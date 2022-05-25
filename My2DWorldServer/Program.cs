using Microsoft.EntityFrameworkCore;
using My2DWorldShared;
using My2DWorldShared.DataEntities;
using My2DWorldServer.Calls;
using My2DWorldShared.Data;
using System.Text;
using System.Text.Json;
using My2DWorldShared.PacketsIn;
using My2DWorldServer.Services;

var builder = WebApplication.CreateBuilder(args);

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
