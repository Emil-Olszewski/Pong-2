using System;
using System.Linq;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace Pong_SFML.Game.AudioSystem
{
    public class Spectrum : Transformable, Drawable
    {
        public Color Color { get; set; }
        static List<RectangleShape> _rectangles;

        public Spectrum(int number) => CreateRectangles(number);

        private void CreateRectangles(int number)
        {
            _rectangles = new List<RectangleShape>();
            for(int i=0; i<number; i++)
            {
                _rectangles.Add(new RectangleShape()
                {
                    Size = new Vector2f(GameConfig.SPECTRUM_BAR_WIDTH, 110),
                    FillColor = new Color(90, 150, 140, 100),
                    Position = new Vector2f(GameConfig.SPECTRUM_BAR_WIDTH * i + GameConfig.SPECTRUM_BAR_SPACE_BETWEEN * i, 0)
                });
            }
        }

        private void ChangeColor()
        {
            Random rand = new Random();
            Color = new Color((byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), 50);
            foreach (RectangleShape rect in _rectangles)
                rect.FillColor = Color;
        }

        public void Update(List<byte> bytes)
        {
            for(int i = 0; i < _rectangles.Count(); i++)
                if(i < bytes.Count())
                {
                    _rectangles[i].Size = new Vector2f(_rectangles[i].Size.X, bytes[i]);
                    if (i < GameConfig.SPECTRUM_COLOR_BARS && bytes[i] == byte.MaxValue)
                        ChangeColor();
                }

            bytes.Clear();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (RectangleShape rect in _rectangles)
                target.Draw(rect);
        }
    }
}
