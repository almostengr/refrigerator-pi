using System.Diagnostics;
using Almostengr.Common.DomainServices.Results;
using Almostengr.Refrigerator.Models;

namespace Almostengr.Refrigerator.Workers;

internal sealed class TemperatureWorker : BaseWorker<TemperatureWorker>
{
    public TemperatureWorker(
        ApplicationDbContext dbContext,
        ILogger<TemperatureWorker> logger
        ) : base(dbContext, logger)
    {
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
                    await _dbContext.SaveChangesAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
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
}
