using Il2CppReloaded.Services;

namespace ReplantAPI.Services
{
    public class AudioServiceWrapper
    {
        private IAudioService GetService()
        {
            return Core.ReplantAPI.Audio;
        }

        public void SetMusicSpeed(float speed)
        {
            var audio = GetService();
            if (audio != null)
                audio.SetMusicSpeed(speed);
        }

        public void EnableBurst()
        {
            var audio = GetService();
            if (audio != null)
            {
                audio.BurstOverride = 1;
                audio.UpdateMusicBurst();
            }
        }

        public void DisableBurst()
        {
            var audio = GetService();
            if (audio != null)
            {
                audio.BurstOverride = 0;
                audio.UpdateMusicBurst();
            }
        }

        public void StartBurst()
        {
            var audio = GetService();
            if (audio != null)
                audio.StartBurst();
        }

        public void PlayMusic(MusicTune tune)
        {
            var audio = GetService();
            if (audio != null)
                audio.PlayMusic(tune, -1, 0);
        }
    }
}