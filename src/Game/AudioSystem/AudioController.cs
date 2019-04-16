﻿using System;
using System.Timers;
using Un4seen.Bass;

namespace Pong_SFML.Game.AudioSystem
{
    public static class AudioController
    {
        public static Spectrum Spectrum;
        
        static Timer _timer;
        static int _music;
        static int _channel;

        static AudioController()
        {
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            Spectrum = new Spectrum(GameConfig.SPECTRUM_LINES);
        }

        public static void LoadMusic(string path)
        {
            _music = Bass.BASS_StreamCreateFile(path, 0L, 0L, BASSFlag.BASS_DEFAULT);
            if (_music != 0)
                SpectrumCreator.Stream = _music;
            else
                throw new Exception(Bass.BASS_ErrorGetCode().ToString());
        }

        public static void PlaySound(string name)
        {
            if(Sounds.GetSoundNamed(name) != 0)
            {
                _channel = Bass.BASS_SampleGetChannel(Sounds.GetSoundNamed(name), false);  
                Bass.BASS_ChannelPlay(_channel, true);
            }
        }

        public static void StartPlayingMusic()
        {
            Bass.BASS_ChannelPlay(_music, false);
            //Bass.BASS_ChannelSetAttribute(_music, BASSAttribute.BASS_ATTRIB_VOL, 0);
            InitTimer();
        }

        public static void Update()
        {
            if (_timer.Enabled)
                Spectrum.Update(SpectrumCreator.Bytes);
        }

        public static double GetRemainedMusicLenght()
            => Bass.BASS_ChannelBytes2Seconds(_music, Bass.BASS_ChannelGetLength(_music) - Bass.BASS_ChannelGetPosition(_music));

        static void InitTimer()
        {
            _timer = new Timer(GameConfig.SPECTRUM_REFRESH);
            _timer.Elapsed += SpectrumCreator.Do;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
    }
}
