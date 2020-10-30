using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LightSourceSearch.Container;
using LightSourceSearch.Services.Config;
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
        private readonly ILogger _logger;

        public EntryPoint(ILoggerFactory loggerFactory, IEnvConfig envConfig, ISpeaker speaker)
        {
            _envConfig = envConfig;
            _speaker = speaker;
            _logger = loggerFactory.GetLogger("Main");
        }
        
        public void Run()
        {
            _logger.Information("Program started");
            
            Pi.Init<BootstrapWiringPi>();
            _logger.Information("Raspberry Pi bootstrapped");
            
            _speaker.BeepAsync(SpeakerSound.Greet);

            _logger.Information("Environment variables:");
            _logger.Information($"TERM={_envConfig.Get("TERM", "{unset}")}");
            _logger.Information($"{EnvVar.PinSpeaker}={_envConfig.Get(EnvVar.PinSpeaker, EnvVar.PinSpeakerDef)}");
            
            Thread.Sleep(1000);
            var sd = DateTime.Now;
            var sound = new SpeakerSound(100, 20)
            {
                Delay = 5
            };
            
            while (DateTime.Now - sd <= TimeSpan.FromMilliseconds(10000))
            {
                _speaker.Beep(sound);
            }

            _logger.Information("Exiting");
        }
    }
}