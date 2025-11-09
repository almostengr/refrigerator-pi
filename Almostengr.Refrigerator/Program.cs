using Almostengr.Refrigerator.Features.Common.Shared;
using Almostengr.Refrigerator.Features.SystemSettings.DomainServices.Interfaces;
using Almostengr.Refrigerator.Features.Temperatures.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommonExtensions(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSystemSettingServices();
builder.Services.AddTemperatureServices();

#if RELEASE
builder.Services.AddHostedService<CompressorWorker>();
builder.Services.AddHostedService<DoorAlarmWorker>();
builder.Services.AddHostedService<DoorStateWorker>();
builder.Services.AddHostedService<TemperatureWorker>();
#endif

var app = builder.Build();

// perform migrations
using (var scope = app.Services.CreateAsyncScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unable to complete application migration: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
