using System.Device.Gpio;
using Almostengr.RefrigeratorPi.Features.Common.Shared;

namespace Almostengr.RefrigeratorPi.Workers;

internal abstract class BaseWorker<TWorker> : BackgroundService
{
    protected readonly GpioController _gpioController;
    protected readonly ILogger<TWorker> _logger;

    public BaseWorker(
        ILogger<TWorker> logger
    )
    {
        _gpioController = new GpioController();
        _logger = logger;
    }

    protected PinValue ReadInput(GpioPinOption pin)
    {
        int pinNumber = (int)pin;

        if (!_gpioController.IsPinOpen(pinNumber))
        {
            _gpioController.OpenPin(pinNumber);
        }

        return _gpioController.Read(pinNumber);
    }

    protected void WriteOutput(GpioPinOption pin, PinValue pinValue)
    {
        int pinNumber = (int)pin;

        if (!_gpioController.IsPinOpen(pinNumber))
        {
            _gpioController.OpenPin(pinNumber);
        }

        _gpioController.Write(pinNumber, pinValue);
    }
}