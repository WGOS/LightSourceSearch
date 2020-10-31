using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightSourceSearch.Services.SpeakerService
{
    public interface ISpeaker
    {
        void Initialize();
        void Beep(SpeakerSound sound);
        void Beep(List<SpeakerSound> sounds);
        Task BeepAsync(SpeakerSound sound);
        Task BeepAsync(List<SpeakerSound> sounds);
    }
}