using Pong_SFML.Components;
using Pong_SFML.Configs;
using SFML.Graphics;
using SFML_UI.Components;

namespace Pong_SFML.Menu.Screens
{
    class HelpScreen : Screen
    {
        Label _help;
        Label _p1Info;
        Label [] _p1Controls;
        Label _p2Info;
        Label [] _p2Controls;
        Label _info;
        Label _infoContent;
        Button _back;

        public HelpScreen(RenderWindow win) : base(win)
        {
            _help = new Label(new Text("Help", Fonts.InGameFont, 80), new Color(175, 206, 255));

            _p1Info = new Label(new Text("Player 1 controls", Fonts.MenuFont, 35), new Color(175, 206, 255));
            _p1Controls = new Label[] 
            {
                new Label(new Text("W A S D - move", Fonts.MenuFont, 30), Color.Black),
                new Label(new Text("R - boost", Fonts.MenuFont, 30), Color.Black),
                new Label(new Text("T - transparent", Fonts.MenuFont, 30), Color.Black),
            };

            _p2Info = new Label(new Text("Player 2 controls", Fonts.MenuFont, 35), new Color(175, 206, 255));
            _p2Controls = new Label[] 
            {
                new Label(new Text("Arrows - move", Fonts.MenuFont, 30), Color.Black),
                new Label(new Text("Num1 - boost", Fonts.MenuFont, 30), Color.Black),
                new Label(new Text("Num2 - transparent", Fonts.MenuFont, 30), Color.Black),
            };

            _info = new Label(new Text("PRO-TIP", Fonts.MenuFont, 35), new Color(175, 206, 255));
            _infoContent = new Label(new Text("There is no limit on bonuses for one activation!", Fonts.MenuFont, 30), Color.Black);
            _back = new Button(new Text("Back", Fonts.MenuFont, 35), MenuController.ShowMainMenu);

            VBox _right = new VBox(20, _p1Info, _p1Controls[0], _p1Controls[1], _p1Controls[2]);
            VBox _left = new VBox(20, _p2Info, _p2Controls[0], _p2Controls[1], _p2Controls[2]);

            HBox _rightleft = new HBox(100, _right, _left);
            VBox _tip = new VBox(30, _info, _infoContent);
            VBox _main = new VBox(60, _help, _rightleft, _tip, _back);

            _interactivables.Add(_back);
            _boxes.Add(_main);
            Center(_main);
        }
    }
}
