using System;
using System.Timers;
using Pong_SFML.Menu;
using Pong_SFML.Menu.Components;

namespace Pong_SFML.Components
{
    public static class MenuController
    {
        private static Timer _timer = new Timer(3000);
        private static TitleScreen _titleScreen = new TitleScreen();
        private static MainMenuScreen _mainMenuScreen = new MainMenuScreen();
        private static HelpScreen _helpScreen = new HelpScreen();
        private static Screen _actualScreen;

        static MenuController()
        {
            _timer.Elapsed += ShowMainMenu;
            _timer.AutoReset = false;
            _timer.Start();
            ShowTitleScreen();
        }

        private static void ShowTitleScreen() => _actualScreen = _titleScreen;

        public static void ShowMainMenu(object sender, EventArgs e) 
            => _actualScreen = _mainMenuScreen;

        public static void ShowHelp(object sender, EventArgs e)
            => _actualScreen = _helpScreen;

        public static void Update()
        {
            MainWindow.Win.Draw(_actualScreen);
            _actualScreen?.Update();
        }
    }
}
