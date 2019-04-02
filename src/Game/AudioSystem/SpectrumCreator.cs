using System;
using System.Collections.Generic;
using System.Timers;
using Un4seen.Bass;

namespace Pong_SFML.Game.AudioSystem
{
    public static class SpectrumCreator
    {
        public static int Stream { get; set; }
        public static List<byte> Bytes = new List<byte>();
        static readonly float[] _fft = new float[8092];

        public static void Do(Object source, ElapsedEventArgs e)
        {
            int ret = Bass.BASS_ChannelGetData(Stream, _fft, (int)(BASSData.BASS_DATA_FFT8192 | BASSData.BASS_DATA_FFT_REMOVEDC));
            if (ret < -1)
                return;

            int x, y, b0 = 0;
            for (x = 0; x < GameConfig.SPECTRUM_LINES; x++)
            {
                float peak = 0;
                int b1 = (int)Math.Pow(2, x * 10.0 / (GameConfig.SPECTRUM_LINES - 1));
                if (b1 > 1023)
                    b1 = 1023;
                if (b1 <= b0)
                    b1 = b0 + 1;
                for (; b0 < b1; b0++)
                    if (peak < _fft[1 + b0])
                        peak = _fft[1 + b0];

                y = (int)(Math.Sqrt(peak) * 3 * 255 - 4);
                if (y > 255)
                    y = 255;
                else if (y < 0)
                    y = 0;

                Bytes.Add((byte)y);
            }
        }
    }
}
