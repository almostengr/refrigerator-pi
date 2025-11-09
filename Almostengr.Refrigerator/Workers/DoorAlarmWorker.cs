using System.Device.Gpio;
using Almostengr.Refrigerator.Features.Common.Shared;
using Almostengr.Refrigerator.Features.SystemSettings.Domain;
using Almostengr.Refrigerator.Features.SystemSettings.Services.Interfaces;
using Almostengr.Refrigerator.Models;

namespace Almostengr.Refrigerator.Workers;

internal sealed class DoorAlarmWorker : BaseWorker<DoorAlarmWorker>
{
    private readonly IQuerySystemSettingService _systemSettingService;

    public DoorAlarmWorker(
        ILogger<DoorAlarmWorker> logger,
        IQuerySystemSettingService systemSettingService
        ) : base(logger)
    {
        _systemSettingService = systemSettingService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                SystemSettingEntity timeoutSetting = await _systemSettingService.GetEntityByOptionAsync(SystemSettingOption.DoorAlarmSeconds);
                if (timeoutSetting.IntValue() == 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(60));
                    continue;
                }

                if (FridgeStateModel.IsDoorOpen && (FridgeStateModel.DoorLastOpened - DateTime.Now) >= TimeSpan.FromMinutes(timeoutSetting.IntValue()))
                {
                    WriteOutput(GpioPinOption.DoorAlarm, PinValue.High);
                }
                else
                {
                    WriteOutput(GpioPinOption.DoorAlarm, PinValue.Low);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CommonExtensions.LOG_MESSAGE, ex.Message);
            }

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}