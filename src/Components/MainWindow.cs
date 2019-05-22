using System;
using SFML.Window;
using SFML.Graphics;
using Pong_SFML.Configs;
using Pong_SFML.Components.Controls;

namespace Pong_SFML.Components
{
    static class MainWindow
    {
        public static RenderWindow Win { get; }

        static MainWindow()
        {
            Win = new RenderWindow(new VideoMode(WindowConfig.WIDTH, WindowConfig.HEIGHT), WindowConfig.TITLE);
            Win.SetFramerateLimit(WindowConfig.FRAMERATE);

            Win.KeyPressed += Win_KeyPressed;
            Win.KeyReleased += Win_KeyReleased;
            Win.Closed += Window_Closed;
        }

        private static void Win_KeyPressed(object sender, KeyEventArgs e)
        {
            if (KeysInUse.Holding.Contains(e.Code) && !KeysInfo.Holded.Contains(e.Code))
                KeysInfo.Holded.Add(e.Code);

            if (KeysInUse.Pressing.Contains(e.Code))
                KeysInfo.Pressed.Add(e.Code);
        }

        private static void Win_KeyReleased(object sender, KeyEventArgs e)
            => KeysInfo.Holded.Remove(e.Code);

        public static void Window_Closed(object sender, EventArgs e) 
            => Win.Close();
    }
}
