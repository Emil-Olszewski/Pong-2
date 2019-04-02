using System;
using System.Timers;
using Un4seen.Bass;

namespace Pong_SFML.Game.AudioSystem
{
    public static class AudioController
    {
        public static Spectrum Spectrum;
        static Timer _timer;
        static int _stream;

        public static void InitBass(string path)
        {
            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                _stream = Bass.BASS_StreamCreateFile(path, 0L, 0L, BASSFlag.BASS_DEFAULT);
                if (_stream != 0)
                    SpectrumCreator.Stream = _stream;
                else
                    throw new Exception(Bass.BASS_ErrorGetCode().ToString());
            }

            Spectrum = new Spectrum(GameConfig.SPECTRUM_LINES);
        }

        public static void Play()
        {
            Bass.BASS_ChannelPlay(_stream, false);
            InitTimer();
        }

        public static void Update()
        {
            if (_timer.Enabled)
                Spectrum.Update(SpectrumCreator.Bytes);
        }

        static void InitTimer()
        {
            _timer = new Timer(GameConfig.SPECTRUM_REFRESH);
            _timer.Elapsed += SpectrumCreator.Do;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
    }
}
