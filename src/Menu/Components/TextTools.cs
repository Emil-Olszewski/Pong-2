using System.Collections.Generic;
using Pong_SFML.Components;
using Pong_SFML.Configs;
using SFML.Graphics;
using SFML.System;

namespace Pong_SFML.Menu.Components
{
    public static class TextTools
    {
        public static void CenterHorizontally(Text text)
            => text.Position = new Vector2f(WindowConfig.WIDTH / 2 - text.GetGlobalBounds().Width / 2, text.Position.Y);

        public static void CenterHorizontally(List<Text> texts)
        {
            foreach (Text text in texts)
                text.Position = new Vector2f(WindowConfig.WIDTH / 2 - text.GetGlobalBounds().Width / 2, text.Position.Y);
        }

        public static void ColorAll(List<Text> texts, Color color)
        {
            foreach (Text text in texts)
                text.FillColor = color;
        }

        public static void CenterVertically(Text text)
            => text.Position = new Vector2f(text.Position.X, WindowConfig.HEIGHT / 2 - text.GetGlobalBounds().Height / 2);

        public static void CenterVertically(List<Text> texts, int padding, int firstPaddingAddition = 0)
        {
            float sum = 0;
            foreach (Text text in texts)
                sum += text.GetGlobalBounds().Height;
            sum += padding * (texts.Count - 1) + firstPaddingAddition;

            float first = WindowConfig.HEIGHT / 2 - sum / 2;

            for (int i = 0; i < texts.Count; i++)
            {
                float posY = i == 0 ? first : texts[i - 1].Position.Y + texts[i - 1].GetGlobalBounds().Height + padding;
                if (i == 1)
                    posY += firstPaddingAddition;
                texts[i].Position = new Vector2f(texts[i].Position.X, posY);
            }
        }

        public static bool IsTextPointedByMouse(Text text)
        {
            if (text.GetGlobalBounds().Contains(SFML.Window.Mouse.GetPosition(MainWindow.Win).X,
                    SFML.Window.Mouse.GetPosition(MainWindow.Win).Y))
                return true;
            return false;
        }
    }
}
