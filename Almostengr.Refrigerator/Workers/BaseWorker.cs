using System.Device.Gpio;
using Almostengr.Refrigerator.Models;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Refrigerator.Workers;

internal abstract class BaseWorker<TWorker> : BackgroundService
{
    protected readonly ApplicationDbContext _dbContext;
    protected readonly GpioController _gpioController;
    protected readonly ILogger<TWorker> _logger;

    public BaseWorker(
        ApplicationDbContext dbContext,
        ILogger<TWorker> logger
    )
    {
        _dbContext = dbContext;
        _gpioController = new GpioController();
        _logger = logger;
    }

    protected async Task<SystemSettingModel> GetSystemSettingAsync(SystemSettingOption option)
    {
        return await _dbContext.SystemSettings
            .AsNoTracking()
            .Where(s => s.Id == (int)option)
            .SingleOrDefaultAsync();
    }

    protected PinValue ReadInput(int pinNumber)
    {
        if (pinNumber < 1 || pinNumber > 30)
        {
            return PinValue.Low;
        }

        if (!_gpioController.IsPinOpen(pinNumber))
        {
            _gpioController.OpenPin(pinNumber, PinMode.InputPullUp);
        }

        return _gpioController.Read(pinNumber);
    }

    protected void WriteOutput(int pinNumber, PinValue pinValue)
    {
        if (pinNumber < 1 || pinNumber > 30)
        {
            return;
        }

        if (!_gpioController.IsPinOpen(pinNumber))
        {
            _gpioController.OpenPin(pinNumber, PinMode.Output);
        }

        _gpioController.Write(pinNumber, pinValue);
    }
}