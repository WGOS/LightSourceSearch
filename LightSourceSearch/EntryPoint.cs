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
            _logger.Information("Application started");
            
            Pi.Init<BootstrapWiringPi>();
            _logger.Information("Raspberry Pi bootstrapped");
            _logger.Information($"Raspberry Model: {Pi.Info.RaspberryPiVersion}");
            
            _speaker.Initialize();
            _laser.Initialize();
            
            _speaker.BeepAsync(SpeakerSound.Greet);
            _logger.Information("Application ready");
            
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                _laser.Turned = key.Key switch
                {
                    ConsoleKey.D1 => true,
                    ConsoleKey.D2 => false,
                    _ => _laser.Turned
                };
                
                _speaker.BeepAsync(SpeakerSound.Beep);
            } while (key.Key != ConsoleKey.Enter);

            _logger.Information("Exiting");
            _laser.Turned = false;
            _speaker.Beep(SpeakerSound.Bye);
        }
    }
}