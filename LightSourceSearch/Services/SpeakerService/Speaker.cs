using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LightSourceSearch.Services.Config;
using LightSourceSearch.Services.Logging;
using Serilog;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;

namespace LightSourceSearch.Services.Speaker
{
    public class Speaker : ISpeaker
    {
        private GpioPin _pin;
        private readonly IEnvConfig _envConfig;
        private readonly ILogger _logger;

        public Speaker(ILoggerFactory loggerFactory, IEnvConfig envConfig)
        {
            _envConfig = envConfig;
            _logger = loggerFactory.GetLogger("Speaker");
        }

        public void Initialize()
        {
            _pin = (GpioPin) Pi.Gpio[_envConfig.Get(EnvVar.PinSpeaker, EnvVar.PinSpeakerDef)];
            _logger.Information($"Pin: {_pin.BcmPin}");
        }

        public void Beep(SpeakerSound sound)
        {
            var currentTone = sound.Tone;
            
            for (var i = 0; i < sound.Repeat; i++)
            {
                _pin.SoftToneFrequency = currentTone;
                Thread.Sleep(sound.Length);

                currentTone += sound.Increase;

                if (sound.Delay <= 0) continue;
                
                _pin.SoftToneFrequency = 0;
                Thread.Sleep(sound.Delay);
            }
            
            _pin.SoftToneFrequency = 0;
            if(sound.SeqDelay > 0)
                Thread.Sleep(sound.SeqDelay);
        }

        public void Beep(List<SpeakerSound> sounds)
        {
            sounds.ForEach(Beep);
        }

        public async Task BeepAsync(SpeakerSound sound)
        {
            await Task.Run(() => Beep(sound));
        }
        
        public async Task BeepAsync(List<SpeakerSound> sounds)
        {
            await Task.Run(() => Beep(sounds));
        }
    }
}