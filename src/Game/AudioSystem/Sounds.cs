using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong_SFML.Game.AudioSystem
{
    static class Sounds
    {
        public static List<Sound> SoundsList = new List<Sound>()
        {
            new Sound("Resources/sounds/hit.wav", "HIT"),
            new Sound("Resources/sounds/power.wav", "POWER"),
            new Sound("Resources/sounds/score.wav", "SCORE"),
            new Sound("Resources/sounds/explosion.wav", "EXPLOSION"),
            new Sound("Resources/sounds/rikochet.wav", "RIKOCHET"),
        };

        public static int GetSoundNamed(string name)
        {
            foreach (Sound sound in SoundsList)
                if (sound.Name == name) return sound.Sample;
            return 0;
        }
    }
}
