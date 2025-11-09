using System.Device.Gpio;
using Almostengr.Refrigerator.Features.Common.Shared;
using Almostengr.Refrigerator.Features.Compressors.DomainServices.Interfaces;
using Almostengr.Refrigerator.Features.SystemSettings.Domain;
using Almostengr.Refrigerator.Features.SystemSettings.Services.Interfaces;
using Almostengr.Refrigerator.Features.Temperatures.Services.interfaces;
using Almostengr.Refrigerator.Models;

namespace Almostengr.Refrigerator.Workers;

internal sealed class CompressorWorker : BaseWorker<CompressorWorker>
{
    private readonly IAddCompressorHistoryService _addCompressorHistoryService;
    private readonly IQuerySystemSettingService _querySystemSettingService;
    private readonly IQueryTemperatureService _queryTemperatureService;
    private readonly IUpdateCompressorHistoryService _updateCompressorHistoryService;

    public CompressorWorker(
        IAddCompressorHistoryService addCompressorHistoryService,
        ILogger<CompressorWorker> logger,
        IQuerySystemSettingService querySystemSettingService,
        IQueryTemperatureService queryTemperatureService,
        IUpdateCompressorHistoryService updateCompressorHistoryService
        ) : base(logger)
    {
        _addCompressorHistoryService = addCompressorHistoryService;
        _querySystemSettingService = querySystemSettingService;
        _queryTemperatureService = queryTemperatureService;
        _updateCompressorHistoryService = updateCompressorHistoryService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                WriteOutput(GpioPinOption.Defroster, PinValue.Low);

                TemperatureModel latestReading = await _queryTemperatureService.GetLatestAsync();
                SystemSettingEntity minTempSetting = await _querySystemSettingService.GetEntityByOptionAsync(SystemSettingOption.MinimumTemperatureC);
                SystemSettingEntity maxTempSetting = await _querySystemSettingService.GetEntityByOptionAsync(SystemSettingOption.MaximumTemperatureC);

                if (latestReading == null || latestReading.ReadingC >= maxTempSetting.DecimalValue())
                {
                    WriteOutput(GpioPinOption.Compressor, PinValue.High);
                    await _addCompressorHistoryService.ExecuteAsync(CommonExtensions.SYSTEM_USER);
                }
                else if (latestReading.ReadingC <= minTempSetting.DecimalValue())
                {
                    WriteOutput(GpioPinOption.Compressor, PinValue.Low);
                    await _updateCompressorHistoryService.ExecuteAsync(CommonExtensions.SYSTEM_USER);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CommonExtensions.LOG_MESSAGE, ex.Message);
            }

            await Task.Delay(TimeSpan.FromMinutes(5));
        }
    }
}
