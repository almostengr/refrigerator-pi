using System.Device.Gpio;
using Almostengr.Refrigerator.Features.Common.Shared;
using Almostengr.Refrigerator.Features.SystemSettings.Domain;
using Almostengr.Refrigerator.Features.SystemSettings.Services.Interfaces;
using Almostengr.Refrigerator.Models;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Refrigerator.Workers;

internal sealed class CompressorWorker : BaseWorker<CompressorWorker>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IQuerySystemSettingService _systemSettingService;

    public CompressorWorker(
        ApplicationDbContext dbContext,
        ILogger<CompressorWorker> logger,
        IQuerySystemSettingService systemSettingService
        ) : base( logger)
    {
        _dbContext = dbContext;
        _systemSettingService = systemSettingService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                WriteOutput(GpioPinOption.Defroster, PinValue.Low);

                TemperatureModel latestReading = await _dbContext.Temperatures
                    .AsNoTracking()
                    .Where(t => t.ModifiedDate >= DateTime.Now.AddMinutes(-10))
                    .OrderByDescending(t => t.Id)
                    .FirstOrDefaultAsync();
                SystemSettingEntity minTempSetting = await _systemSettingService.GetEntityByOptionAsync(SystemSettingOption.MinimumTemperatureC);
                SystemSettingEntity maxTempSetting = await _systemSettingService.GetEntityByOptionAsync(SystemSettingOption.MaximumTemperatureC);

                if (latestReading == null || latestReading.ReadingC >= maxTempSetting.DecimalValue())
                {
                    WriteOutput(GpioPinOption.Compressor, PinValue.High);
                }
                else if (latestReading.ReadingC <= minTempSetting.DecimalValue())
                {
                    WriteOutput(GpioPinOption.Compressor, PinValue.Low);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            await Task.Delay(TimeSpan.FromMinutes(5));
        }
    }
}
