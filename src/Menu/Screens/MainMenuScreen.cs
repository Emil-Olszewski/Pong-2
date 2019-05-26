using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML_UI.Components;
using Pong_SFML.Game;
using Pong_SFML.Game.AudioSystem;
using Pong_SFML.Game.AudioSystem.Types;
using Pong_SFML.Configs;
using Pong_SFML.Components;

namespace Pong_SFML.Menu.Screens
{
    class MainMenuScreen : Screen
    {
        Label _gameName;

        Button _multiplayerOffline;
        Button _multiplayerOnline;
        Button _help;
        Button _exit;
        VBox _box1;

        Label _songDisplayer;
        Button _nextSong;
        Song _song = Songs.GetRandomSong();
        VBox _box2;

        VBox _mainBox;

        public MainMenuScreen(RenderWindow win) : base(win)
        {
            _gameName = new Label(new Text("Pong 2", Fonts.InGameFont, 80), new Color(175, 206, 255));

            _multiplayerOffline = new Button(new Text("2 players mode", Fonts.MenuFont, 35), MainController.RunGame);
            _multiplayerOnline = new Button(new Text("Multiplayer", Fonts.MenuFont, 35), MenuController.ShowMultiplayer);
            _help = new Button(new Text("Help", Fonts.MenuFont, 35), MenuController.ShowHelp);
            _exit = new Button(new Text("Exit", Fonts.MenuFont, 35), MainWindow.Window_Closed);

            _box1 = new VBox(20, _multiplayerOffline, _multiplayerOnline, _help, _exit);

            _songDisplayer = new Label(new Text(_song.DisplayName, Fonts.InGameFont, 20), new Color(147, 179, 226));
            _nextSong = new Button(new Text("NEXT", Fonts.MenuFont, 20), ChangeSong);

            _box2 = new VBox(20, _songDisplayer, _nextSong);
            _mainBox = new VBox(50, _gameName, _box1, _box2);
            _boxes.Add(_mainBox);

            Center(_mainBox);

            _interactivables = new List<IInteractivable>() { _multiplayerOffline, _multiplayerOnline, _help, _exit, _nextSong };
            GameController.Song = _song;
        }

        private void ChangeSong(object sender, EventArgs e)
        {
            int number = Songs.List.IndexOf(_song) + 1;
            if (number >= Songs.List.Count)
                number = 0;

            _song = Songs.List[number];
            _songDisplayer.SetString(_song.DisplayName);
            GameController.Song = _song;

            _mainBox.Refresh();
            Center(_mainBox);
        }
    }
}
