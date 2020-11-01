using LightSourceSearch.Misc;
using LightSourceSearch.Services.Logging;
using Serilog;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;

namespace LightSourceSearch.Services.LaserService
{
    public class Laser : ILaser
    {
        private readonly ILogger _logger;
        private IGpioPin _pin;

        public Laser(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger("Laser");
        }

        public bool Turned
        {
            get => _pin.Value;
            set
            {
                _logger.Information(value ? "Turned on laser" : "Turned off laser");
                _pin.Value = value;
            }
        }

        public void Initialize()
        {
            _logger.Information("Initializing");
            _pin = Pi.Gpio[EnvVar.LaserPin.Value];
            _pin.PinMode = GpioPinDriveMode.Output;

            _logger.Information($"Pin: {_pin.BcmPin}");
        }
    }
}