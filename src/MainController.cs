using System;
using SFML.Window;
using Pong_SFML.Menu;
using Pong_SFML.Game;
using Pong_SFML.Multiplayer;
using Pong_SFML.Components;
using Pong_SFML.Components.KeyboardHandle;

namespace Pong_SFML
{
    public static class MainController
    {
        private enum State { MENU, GAME};
        private static State _state = State.MENU;

        public static void Main(string[] args)
        {
            SocketReceiver.StartListener();
            while (MainWindow.Win.IsOpen)
            {
                MainWindow.Win.DispatchEvents();
                MainWindow.Clear();

                if (_state == State.MENU)
                    MenuController.Update();
                else
                {
                    GameController.Update();
                }

                if (KeysInfo.Pressed.Contains(Keyboard.Key.Escape))
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
