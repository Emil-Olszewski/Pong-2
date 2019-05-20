using System.Collections.Generic;
using SFML.Graphics;
using Pong_SFML.Menu.Components;
using Pong_SFML.Configs;

namespace Pong_SFML.Menu
{
    class TitleScreen : Screen
    {
        private Text _author = new Text("Emil Olszewski 2019", Fonts.MenuFont, 30);
        private Text _version = new Text("Version 1.0", Fonts.MenuFont, 20);

        public TitleScreen()
        {
             _texts = new List<Text>() { _author, _version };
            TextTools.ColorAll(_texts, Color.Black);
            _texts.Insert(0, _gameName);
            TextTools.CenterHorizontally(_texts);
            TextTools.CenterVertically(_texts, 20, 30);
        }
    }
}
