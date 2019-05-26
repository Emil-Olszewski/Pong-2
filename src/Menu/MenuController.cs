using System;
using System.Timers;
using SFML_UI.Components;
using Pong_SFML.Menu.Screens;
using Pong_SFML.Components;
using Pong_SFML.Components.KeyboardHandle;

namespace Pong_SFML.Menu
{
    public static class MenuController
    {
        private static Timer _timer = new Timer(3000);
        private static TitleScreen _titleScreen = new TitleScreen(MainWindow.Win);
        private static MainMenuScreen _menuScreen = new MainMenuScreen(MainWindow.Win);
        private static MultiplayerScreen _multiplayerScreen = new MultiplayerScreen(MainWindow.Win);
        private static HelpScreen _helpScreen = new HelpScreen(MainWindow.Win);
        private static Screen _actualScreen;

        static MenuController()
        {
            _timer.Elapsed += ShowMainMenu;
            _timer.AutoReset = false;
            _timer.Start();
            ShowTitleScreen();

            SFML_UI.Keyboard.KeysInfo.Pressed = KeysInfo.Pressed;
        }

        private static void ShowTitleScreen() 
            => _actualScreen = _titleScreen;

        public static void ShowMainMenu(object sender, EventArgs e)
            => _actualScreen = _menuScreen;

        public static void ShowMultiplayer(object sender, EventArgs e)
            => _actualScreen = _multiplayerScreen;

        public static void ShowHelp(object sender, EventArgs e)
            => _actualScreen = _helpScreen;

        public static void Update()
        {
            MainWindow.BackgroundColor = _actualScreen.BackgroundColor;
            MainWindow.Win.Draw(_actualScreen);
            _actualScreen?.Update();
        }
    }
}
