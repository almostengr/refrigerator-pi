using System.Device.Gpio;
using Almostengr.Refrigerator.Models;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Refrigerator.Workers;

internal sealed class CompressorWorker : BaseWorker<CompressorWorker>
{
    public CompressorWorker(ApplicationDbContext dbContext,
        ILogger<CompressorWorker> logger) : base(dbContext, logger)
    {
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
                SystemSettingModel minTempSetting = await GetSystemSettingAsync(SystemSettingOption.MinimumTemperatureC);
                SystemSettingModel maxTempSetting = await GetSystemSettingAsync(SystemSettingOption.MaximumTemperatureC);

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
