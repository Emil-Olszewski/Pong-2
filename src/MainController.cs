using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using Pong_SFML.Configs;

namespace Pong_SFML
{
    public static class MainController
    {
        private static RenderWindow Win;
        private static GameController GameCtrl = new GameController();
        
        private static List<DescrKey> _holdingKeysPressed = new List<DescrKey>();
        private static List<DescrKey> _pressingKeysPressed = new List<DescrKey>();

        public static void Main(string[] args)
        {
            Win = new RenderWindow(new VideoMode(WindowConfig.WIDTH, WindowConfig.HEIGHT), WindowConfig.TITLE);
            Win.SetFramerateLimit(WindowConfig.FRAMERATE);

            Win.KeyPressed += Win_KeyPressed;
            Win.KeyReleased += Win_KeyReleased;
            Win.Closed += Window_Closed;

            GameCtrl.Run("nyan");

            while (Win.IsOpen)
            {
                Win.DispatchEvents();
                Win.Clear(GameCtrl.BackgroundColor);
           
                GameCtrl.Update(Win);

                foreach (DescrKey key in _holdingKeysPressed)
                    GameCtrl.ReactTo(key);

                foreach (DescrKey key in _pressingKeysPressed)
                    GameCtrl.ReactTo(key);

                _pressingKeysPressed.Clear();

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    Win.Close();

                Win.Display();
            }
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
            for(int i=0; i<_holdingKeysPressed.Count; i++)
                if (!Keyboard.IsKeyPressed(_holdingKeysPressed[i].Key))
                    _holdingKeysPressed.Remove(_holdingKeysPressed[i]);
        }

        private static void Window_Closed(object sender, EventArgs e) => Win.Close();
    }
}
