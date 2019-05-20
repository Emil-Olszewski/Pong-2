using Un4seen.Bass;

namespace Pong_SFML.Game.AudioSystem.Types
{
    public class Sound
    {
        public string Path;
        public string Name;
        public int Sample;

        public Sound(string path, string name)
        {
            Path = path;
            Name = name;
            Sample = Bass.BASS_SampleLoad(path, 0, 0, 1, BASSFlag.BASS_SAMPLE_FLOAT);
        }
    }
}
