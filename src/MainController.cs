using System;
using SFML.Window;
using Pong_SFML.Configs;

namespace Pong_SFML
{
    public static class MainController
    {
        private enum State { MENU, GAME};
        private static State _state = State.MENU;

        public static void Main(string[] args)
        {
            while (MainWindow.Win.IsOpen)
            {
                MainWindow.Win.DispatchEvents();
                MainWindow.Win.Clear(GameController.BackgroundColor);

                if (_state == State.MENU)
                    MenuController.Update();
                else
                {
                    GameController.Update();

                    foreach (DescrKey key in MainWindow._holdingKeysPressed)
                        GameController.ReactTo(key);

                    foreach (DescrKey key in MainWindow._pressingKeysPressed)
                        GameController.ReactTo(key);
                }

                MainWindow._pressingKeysPressed.Clear();
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    MainWindow.Win.Close();

                MainWindow.Win.Display();
            }
        }

        public static void RunGame(object sender, EventArgs e)
        {
            _state = State.GAME;
            GameController.Run();
        }

        public static void RunMenu()
            => _state = State.MENU;
    }
}
