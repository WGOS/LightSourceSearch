using LightSourceSearch.Services.Config;
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
        private readonly IEnvConfig _envConfig;

        public bool Turned
        {
            get => _pin.Value;
            set => _pin.Value = value;
        }
        
        public Laser(ILoggerFactory loggerFactory, IEnvConfig envConfig)
        {
            _envConfig = envConfig;
            _logger = loggerFactory.GetLogger("Laser");
        }
        
        public void Initialize()
        {
            _logger.Information("Initializing");
            _pin = Pi.Gpio[_envConfig.Get(EnvVar.PinLaser, EnvVar.PinLaserDef)];
            _pin.PinMode = GpioPinDriveMode.Output;
            
            _logger.Information($"Pin: {_pin.BcmPin}");
        }
    }
}