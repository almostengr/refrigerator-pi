using System.Diagnostics;
using Almostengr.Common.DomainServices.Results;
using Almostengr.Refrigerator.Features.Common.Shared;
using Almostengr.Refrigerator.Features.SystemSettings.Domain;
using Almostengr.Refrigerator.Features.SystemSettings.Services.Interfaces;
using Almostengr.Refrigerator.Features.Temperatures.Services.interfaces;
using Almostengr.Refrigerator.Models;

namespace Almostengr.Refrigerator.Workers;

internal sealed class TemperatureWorker : BaseWorker<TemperatureWorker>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IQuerySystemSettingService _querySystemSettingService;
    private readonly IQueryTemperatureService _queryTemperatureService;

    public TemperatureWorker(
        ApplicationDbContext dbContext,
        ILogger<TemperatureWorker> logger,
        IQuerySystemSettingService querySystemSettingService,
        IQueryTemperatureService queryTemperatureService
        ) : base(logger)
    {
        _dbContext = dbContext;
        _querySystemSettingService = querySystemSettingService;
        _queryTemperatureService = queryTemperatureService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                string reading = GetDs18b20Data();

                bool succeeded = decimal.TryParse(reading, out decimal tempC);
                if (!succeeded)
                {
                    await Task.Delay(TimeSpan.FromSeconds(15));
                    continue;
                }

                Result<TemperatureModel> result = TemperatureModel.Create(tempC, "SYSTEM");
                if (result.Succeeded)
                {
                    await _dbContext.Temperatures.AddAsync(result.Value, stoppingToken);
                }

                await RemoveOldReadingsAsync();
                await _dbContext.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CommonExtensions.LOG_MESSAGE, ex.Message);
            }

            await Task.Delay(TimeSpan.FromMinutes(5));
        }
    }

    private string GetDs18b20Data()
    {
        Process process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = "/usr/bin/digitemp_DS9097",
                Arguments = $"-a -q -c /etc/digitemp.conf -o \"%.2C\"",
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };

        process.Start();
        process.WaitForExit();

        string output = process.StandardOutput.ReadToEnd();
        return output;
    }

    private async Task RemoveOldReadingsAsync()
    {
        SystemSettingEntity daysSetting = await _querySystemSettingService.GetEntityByOptionAsync(SystemSettingOption.TemperatureReadingDays);
        if (daysSetting.IntValue() == 0)
        {
            return;
        }

        IList<TemperatureModel> entities = await _queryTemperatureService.GetListByDateRangeAsync(daysSetting.IntValue());
        _dbContext.Temperatures.RemoveRange(entities);
    }
}
