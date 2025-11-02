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
                SystemSettingModel compressorSetting = await GetSystemSettingAsync(SystemSettingOption.CompressorGpio);
                SystemSettingModel defrostSetting = await GetSystemSettingAsync(SystemSettingOption.DefrostGpio);

                if (compressorSetting.IntValue() > 0 && (FridgeStateModel.LastDefrosted - DateTime.Now) >= TimeSpan.FromHours(8))
                {
                    WriteOutput(compressorSetting.IntValue(), PinValue.Low);
                    WriteOutput(defrostSetting.IntValue(), PinValue.High);

                    SystemSettingModel defrostTimeSetting = await GetSystemSettingAsync(SystemSettingOption.DefrostMinutes);
                    await Task.Delay(TimeSpan.FromMinutes(defrostTimeSetting.IntValue()));
                }
                else
                {
                    WriteOutput(defrostSetting.IntValue(), PinValue.Low);

                    TemperatureModel latestReading = await _dbContext.Temperatures.AsNoTracking().OrderByDescending(t => t.Id).FirstOrDefaultAsync();
                    SystemSettingModel minTempSetting = await GetSystemSettingAsync(SystemSettingOption.MinimumTemperatureC);
                    SystemSettingModel maxTempSetting = await GetSystemSettingAsync(SystemSettingOption.MaximumTemperatureC);

                    if (latestReading == null || latestReading.ReadingC >= maxTempSetting.DecimalValue())
                    {
                        WriteOutput(compressorSetting.IntValue(), PinValue.High);
                    }
                    else if (latestReading.ReadingC <= minTempSetting.DecimalValue())
                    {
                        WriteOutput(compressorSetting.IntValue(), PinValue.Low);
                    }
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
