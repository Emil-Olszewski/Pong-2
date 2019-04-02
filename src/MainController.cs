using System;
using System.Collections.Generic;
using SFML.Graphics;
using Pong_SFML.Configs;

namespace Pong_SFML
{
    public static class MainController
    {
        private static RenderWindow Win;
        private static GameController GameCtrl;
        
        private static List<SFML.Window.Keyboard.Key> keysPressed;

        static MainController()
        {
            GameCtrl = new GameController();
            keysPressed = new List<SFML.Window.Keyboard.Key>();
        }

        public static void Main(string[] args)
        {
            Win = new RenderWindow(new SFML.Window.VideoMode(WindowConfig.Width, WindowConfig.Height), WindowConfig.Title);
            Win.SetFramerateLimit(WindowConfig.Framerate);

            GameCtrl.Run();

            Win.KeyPressed += Win_KeyPressed;
            Win.KeyReleased += Win_KeyReleased;
            Win.Closed += Window_Closed;

            while (Win.IsOpen)
            {
                Win.DispatchEvents();
                Win.Clear(GameCtrl.BackgroundColor);
                GameCtrl.Update(Win, keysPressed);

                foreach (SFML.Window.Keyboard.Key key in keysPressed)
                    GameCtrl.ReactTo(key);

                Win.Display();
            }
        }
        
        private static void Win_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            foreach (SFML.Window.Keyboard.Key key in KeyConfig.KeysInUse)
                if (!keysPressed.Contains(key) && SFML.Window.Keyboard.IsKeyPressed(key))
                    keysPressed.Add(key); 
        }

        private static void Win_KeyReleased(object sender, SFML.Window.KeyEventArgs e)
        {
            for(int i=0; i<keysPressed.Count; i++)
                if (!SFML.Window.Keyboard.IsKeyPressed(keysPressed[i]))
                    keysPressed.Remove(keysPressed[i]);
        }

        private static void Window_Closed(object sender, EventArgs e) => Win.Close();
    }
}
