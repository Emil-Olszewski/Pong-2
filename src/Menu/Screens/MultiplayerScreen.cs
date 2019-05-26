using Pong_SFML.Components;
using Pong_SFML.Configs;
using SFML.Graphics;
using SFML_UI.Components;
using System.Collections.Generic;

namespace Pong_SFML.Menu.Screens
{
    class MultiplayerScreen : Screen
    {
        Label _multiplayer;
        Label _info;
        TextBox _ip;
        Button _create;
        Button _join;
        Button _back;


        public MultiplayerScreen(RenderWindow win) : base(win)
        {
            _multiplayer = new Label(new Text("Multiplayer", Fonts.InGameFont, 80), new Color(175, 206, 255));
            _info = new Label(new Text("Write IP below", Fonts.MenuFont, 30), Color.Black);
            _ip = new TextBox(new Text("127.0.0.1", Fonts.InGameFont, 20), 300);

            _create = new Button(new Text("Create", Fonts.MenuFont, 35));
            _join = new Button(new Text("Join", Fonts.MenuFont, 35));
            _back = new Button(new Text("Back", Fonts.MenuFont, 35), MenuController.ShowMainMenu);

            HBox hBox = new HBox(80, _join, _create);
            VBox ip = new VBox(20, _info, _ip, hBox);
            VBox main = new VBox(80, _multiplayer, ip, _back);

            _boxes.Add(main);
            _interactivables = new List<IInteractivable>() { _ip, _create, _join, _back };
            _txtBoxes = new List<TextBox>() { _ip };
            Center(main);
        }
    }
}
