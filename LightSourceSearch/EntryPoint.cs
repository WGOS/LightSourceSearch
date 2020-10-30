using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LightSourceSearch.Container;
using LightSourceSearch.Services.Config;
using LightSourceSearch.Services.LaserService;
using LightSourceSearch.Services.Logging;
using LightSourceSearch.Services.Speaker;
using Serilog;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;

namespace LightSourceSearch
{
    internal class EntryPoint : IContainerEntryPoint
    {
        private readonly IEnvConfig _envConfig;
        private readonly ISpeaker _speaker;
        private readonly ILaser _laser;
        private readonly ILogger _logger;

        public EntryPoint(ILoggerFactory loggerFactory, IEnvConfig envConfig, ISpeaker speaker, ILaser laser)
        {
            _envConfig = envConfig;
            _speaker = speaker;
            _laser = laser;
            _logger = loggerFactory.GetLogger("Main");
        }
        
        public void Run()
        {
            _logger.Information("Program started");
            
            Pi.Init<BootstrapWiringPi>();
            _logger.Information("Raspberry Pi bootstrapped");
            _speaker.Beep(SpeakerSound.Greet);
            _logger.Information($"Raspberry Model: {Pi.Info.RaspberryPiVersion}");
            
            _laser.Initialize();

            _laser.Turned = true;
            Thread.Sleep(5000);
            _laser.Turned = false;

            _logger.Information("Exiting");
            _speaker.Beep(SpeakerSound.Bye);
        }
    }
}