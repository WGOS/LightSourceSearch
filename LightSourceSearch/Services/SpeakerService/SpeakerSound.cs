namespace LightSourceSearch.Services.SpeakerService
{
    public class SpeakerSound
    {
        public int Tone { get; set; }
        public int Length { get; set; }
        public int Repeat { get; set; } = 1;
        public int Delay { get; set; } = 0;
        public int Increase { get; set; } = 0;
        public int SeqDelay { get; set; } = 0;
        
        public SpeakerSound(int tone, int length)
        {
            Tone = tone;
            Length = length;
        }

        public static readonly SpeakerSound Beep = new SpeakerSound(622, 100)
        {
            SeqDelay = 200
        };
        public static readonly SpeakerSound BeepLong = new SpeakerSound(622, 500)
        {
            SeqDelay = 200
        };
        public static readonly SpeakerSound Greet = new SpeakerSound(600, 100)
        {
            Repeat = 3,
            Delay = 20,
            Increase = 100
        };
        public static readonly SpeakerSound Bye = new SpeakerSound(600, 100)
        {
            Repeat = 3,
            Delay = 20,
            Increase = -100
        };
        public static readonly SpeakerSound Error = new SpeakerSound(250, 100)
        {
            Repeat = 3,
            Delay = 40
        };
        public static readonly SpeakerSound CritError = new SpeakerSound(4000, 200)
        {
            Repeat = 4,
            Delay = 100
        };
        public static readonly SpeakerSound Warn = new SpeakerSound(100, 100)
        {
            Repeat = 2,
            Delay = 40,
            Increase = 50
        };
    }
}