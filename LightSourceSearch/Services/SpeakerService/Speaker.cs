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
        private readonly int _speakerPin;
        private readonly ILogger _logger;

        public Speaker(ILoggerFactory loggerFactory, IEnvConfig envConfig)
        {
            _logger = loggerFactory.GetLogger("Speaker");
            _speakerPin = envConfig.Get(EnvVar.PinSpeaker, EnvVar.PinSpeakerDef);
            
            _logger.Information($"Pin: {_speakerPin}");
        }
        
        public void Beep(SpeakerSound sound)
        {
            var speaker = (GpioPin) Pi.Gpio[_speakerPin];
            var currentTone = sound.Tone;
            
            for (var i = 0; i < sound.Repeat; i++)
            {
                speaker.SoftToneFrequency = currentTone;
                Thread.Sleep(sound.Length);

                currentTone += sound.Increase;

                if (sound.Delay <= 0) continue;
                
                speaker.SoftToneFrequency = 0;
                Thread.Sleep(sound.Delay);
            }
            
            speaker.SoftToneFrequency = 0;
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