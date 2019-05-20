using System;
using System.Collections.Generic;
using SFML.Graphics;
using Pong_SFML.Menu.Components;
using Pong_SFML.Configs;
using SFML.System;
using Pong_SFML.Game.AudioSystem.Types;
using Pong_SFML.Game.AudioSystem;

namespace Pong_SFML.Menu
{
    class MainMenuScreen : Screen
    {
        private Button _play1p = new Button();
        private Button _play2p = new Button();
        private Button _help = new Button();
        private Button _exit = new Button();
        private Button _nextSong = new Button();
        private Song _song = Songs.GetRandomSong();
        private Text _displayedSong;

        public MainMenuScreen()
        { 
            _play1p.Text = new Text("Singleplayer", Fonts.MenuFont, 40);
            _play2p.Text = new Text("Multiplayer", Fonts.MenuFont, 40);
            _help.Text = new Text("Help", Fonts.MenuFont, 40);
            _exit.Text = new Text("Exit", Fonts.MenuFont, 40);
            _nextSong.Text = new Text("NEXT", Fonts.MenuFont, 20);

            _texts = new List<Text>() {};
            _buttons = new List<Button>()
            {
                _play1p, _play2p, _help, _exit
            };

            MoveButtonsToTexts();
            _displayedSong = new Text(_song.DisplayName, Fonts.InGameFont, 20);

            _texts.Insert(0, _gameName);
            TextTools.CenterHorizontally(_texts);
            TextTools.CenterVertically(_texts, 30, 30);

            _play2p.ButtonClicked += MainController.RunGame;
            _help.ButtonClicked += MenuController.ShowHelp;
            _exit.ButtonClicked += MainWindow.Window_Closed;

            _displayedSong.FillColor = Color.Red;
            _nextSong.Text.FillColor = Color.Black;

            _texts.Add(_displayedSong);
            _texts.Add(_nextSong.Text);
            _buttons.Add(_nextSong);

            _texts.Remove(_gameName);
            TextTools.ColorAll(_texts, Color.Black);

            _texts.Insert(0, _gameName);

            TextTools.CenterHorizontally(_displayedSong);
            TextTools.CenterHorizontally(_nextSong.Text);

            _displayedSong.Position = new Vector2f(_displayedSong.Position.X, 600);
            _nextSong.Text.Position = new Vector2f(_nextSong.Text.Position.X, 630);

            _nextSong.ButtonClicked += ChangeSong;
            GameController.Song = _song;

        }

        private void ChangeSong(object sender, EventArgs e)
        {
            int number = Songs.List.IndexOf(_song) + 1;
            if (number >= Songs.List.Count)
                number = 0;
            _song = Songs.List[number];
            _displayedSong.DisplayedString = _song.DisplayName;
            TextTools.CenterHorizontally(_displayedSong);
            GameController.Song = _song;
        }
    }
}
