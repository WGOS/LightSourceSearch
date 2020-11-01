using System.Threading;
using LightSourceSearch.Misc;
using LightSourceSearch.Services.Logging;
using Serilog;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.WiringPi;

namespace LightSourceSearch.Services.LaserService
{
    public class Laser : ILaser
    {
        private IGpioPin _pin;
        private readonly ILogger _logger;

        public bool Turned
        {
            get => _pin.Value;
            set
            {
                _logger.Information(value ? "Turned on laser" : "Turned off laser");
                _pin.Value = value;
            }
        }

        public Laser(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger("Laser");
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