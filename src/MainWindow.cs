using System;
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
using Pong_SFML.Configs;

namespace Pong_SFML
{
    static class MainWindow
    {
        public static RenderWindow Win { get; }
        public static List<DescrKey> _holdingKeysPressed = new List<DescrKey>();
        public static List<DescrKey> _pressingKeysPressed = new List<DescrKey>();

        static MainWindow()
        {
            Win = new RenderWindow(new VideoMode(WindowConfig.WIDTH, WindowConfig.HEIGHT), WindowConfig.TITLE, Styles.Fullscreen);
            Win.SetFramerateLimit(WindowConfig.FRAMERATE);

            Win.KeyPressed += Win_KeyPressed;
            Win.KeyReleased += Win_KeyReleased;
            Win.Closed += Window_Closed;
        }

        private static void Win_KeyPressed(object sender, KeyEventArgs e)
        {
            foreach (DescrKey dkey in KeyConfig.HoldingKeys)
                if (Keyboard.IsKeyPressed(dkey.Key) && !_holdingKeysPressed.Contains(dkey))
                    _holdingKeysPressed.Add(dkey);

            foreach (DescrKey dkey in KeyConfig.PressingKeys)
                if (Keyboard.IsKeyPressed(dkey.Key))
                    _pressingKeysPressed.Add(dkey);
        }

        private static void Win_KeyReleased(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < _holdingKeysPressed.Count; i++)
                if (!Keyboard.IsKeyPressed(_holdingKeysPressed[i].Key))
                    _holdingKeysPressed.Remove(_holdingKeysPressed[i]);
        }

        public static void Window_Closed(object sender, EventArgs e) => Win.Close();
    }
}
