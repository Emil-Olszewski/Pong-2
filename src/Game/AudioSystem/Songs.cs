using System;
using System.Collections.Generic;
using Pong_SFML.Game.AudioSystem.Types;

namespace Pong_SFML.Game.AudioSystem
{
    public static class Songs
    {
        public static List<Song> List = new List<Song>()
        {
            new Song("Resources/music/mus1.mp3", "mus1", "PSY - DADDY"),
            new Song("Resources/music/mus2.mp3", "mus2", "NYAN CAT SONG"),
            new Song("Resources/music/mus3.mp3", "mus3", "THE PRINCE KARMA - LATER BITCHES"),
            new Song("Resources/music/mus4.mp3", "mus4", "TRIPALOSKI"),
            new Song("Resources/music/mus5.mp3", "mus5", "SHANGUY - LA LOUZE"),
            new Song("Resources/music/mus6.mp3", "mus6", "VANCE JOY - RIPTIDE"),
            new Song("Resources/music/mus7.mp3", "mus7", "SWEATER WEATHER"),
        };

        public static Song GetRandomSong()
        {
            Random rand = new Random();
            return List[rand.Next(0, List.Count)];
        }
    }
}
