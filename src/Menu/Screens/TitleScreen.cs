using SFML.Graphics;
using Pong_SFML.Configs;
using SFML_UI.Components;

namespace Pong_SFML.Menu.Screens
{
    class TitleScreen : Screen
    {
        Label _gameName;
        Label _author;
        Label _version;
        VBox vBox;

        public TitleScreen(RenderWindow win) : base(win)
        {
            _gameName = new Label(new Text("Pong 2", Fonts.InGameFont, 80), new Color(175, 206, 255));
            _author = new Label(new Text("Emil Olszewski 2019", Fonts.MenuFont, 30), Color.Black);
            _version = new Label(new Text("Version 1.1", Fonts.MenuFont, 20), Color.Black);

            vBox = new VBox(20, _gameName, _author, _version);
            Center(vBox);
            _boxes.Add(vBox);
        }
    }
}
