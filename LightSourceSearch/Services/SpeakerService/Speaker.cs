using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LightSourceSearch.Misc;
using LightSourceSearch.Services.Logging;
using Serilog;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;

namespace LightSourceSearch.Services.SpeakerService
{
    public class Speaker : ISpeaker
    {
        private readonly ILogger _logger;
        private GpioPin _pin;
        private bool _isSilent;

        public Speaker(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.GetLogger("Speaker");
        }

        public void Initialize()
        {
            _logger.Information("Initializing");
            _pin = (GpioPin) Pi.Gpio[EnvVar.SpeakerPin.Value];
            _logger.Information($"Pin: {_pin.BcmPin}");

            _isSilent = EnvVar.SilentMode.Value;
            if(_isSilent)
                _logger.Warning("APPLICATION IS IN SILENT MODE!");
        }

        public void Beep(SpeakerSound sound)
        {
            if (_isSilent)
                return;

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
            if (sound.SeqDelay > 0)
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