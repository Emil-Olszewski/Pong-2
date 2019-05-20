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
    class HelpScreen : Screen
    {
        private Text _p1Info;
        private Text _p1Controls;
        private Text _p2Info;
        private Text _p2Controls;
        private Text _info;
        private Text _infoContent;
        private Button _back = new Button();

        public HelpScreen()
        {
            _p1Info = new Text("Player One Controls", Fonts.MenuFont, 35);
            _p1Controls = new Text(" W A S D - move \n      R - boost \nT - transparent", Fonts.MenuFont, 30);
            _p2Info = new Text("Player Two Controls", Fonts.MenuFont, 35);
            _p2Controls = new Text("     Arrows - move \n      Num1 - boost \nNum2 - transparent", Fonts.MenuFont, 30);
            _info = new Text("PRO-TIP", Fonts.MenuFont, 35);
            _infoContent = new Text("There is no limit on bonuses for one activation", Fonts.MenuFont, 30);
            _back.Text = new Text("Back", Fonts.MenuFont, 40);

            _texts = new List<Text>() { _gameName, _p1Info, _p1Controls, _p2Info, _p2Controls, _info, _infoContent};
            _buttons = new List<Button>() { _back };

            MoveButtonsToTexts();

            TextTools.CenterHorizontally(_texts);
            TextTools.CenterVertically(_texts, 40, 20);

            _back.ButtonClicked += MenuController.ShowMainMenu;

            _texts.Remove(_gameName);
            TextTools.ColorAll(_texts, Color.Black);
            _p1Info.FillColor = Color.Red;
            _p2Info.FillColor = Color.Red;
            _info.FillColor = Color.Red;

            _texts.Insert(0, _gameName);
        }
    }
}
