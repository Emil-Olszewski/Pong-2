using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Pong_SFML.Game.Systems
{
    class ColorChanger
    {
        public Color TargetColor;
        private int[] actualColorDifference = new int[4];

        public void CountActualColorDifference(Color actualColor, Color targetColor)
        {
            TargetColor = targetColor;
            actualColorDifference = new int[4]
            {
                targetColor.R - actualColor.R,
                targetColor.G - actualColor.G,
                targetColor.B - actualColor.B,
                targetColor.A - actualColor.A,

            };
        }

        public Color Get(Color actualColor)
        {
            if (actualColor != TargetColor)
                return new Color()
                {
                    R = GetModifiedValue(actualColor.R, TargetColor.R, 0),
                    G = GetModifiedValue(actualColor.G, TargetColor.G, 1),
                    B = GetModifiedValue(actualColor.B, TargetColor.B, 2),
                    A = GetModifiedValue(actualColor.A, TargetColor.A, 3)
                };
            else
                return actualColor;
        }

        private byte GetModifiedValue(int value, int targetedValue, int number)
        {
            if (value != targetedValue)
                return (byte)(Math.Abs(targetedValue - value) > 255 * GameConfig.ColorChangerMultiplier ?
                    (value + actualColorDifference[number] * GameConfig.ColorChangerMultiplier) : targetedValue);
            else
                return (byte)targetedValue;
        }
    }
}
