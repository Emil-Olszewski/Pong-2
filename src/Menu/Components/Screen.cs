using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using Pong_SFML.Configs;
using System;

namespace Pong_SFML.Menu.Components
{
    abstract class Screen : Transformable, Drawable
    {
        
        protected List<Text> _texts;
        protected List<Button> _buttons;
        protected RectangleShape _background;
        private Button _nowSelected;
        private bool _clicked;
        
        protected Text _gameName;
        
        public Screen()
        {
            _background = new RectangleShape(new Vector2f(WindowConfig.WIDTH, WindowConfig.HEIGHT))
            {
                FillColor = Color.White
            };
            
            _texts = new List<Text>();
            _buttons = new List<Button>();

            _gameName = new Text(WindowConfig.TITLE, Fonts.InGameFont, 70)
            {
                FillColor = new Color(175, 206, 255)
            };
        }

        public void Update()
        {
            HoverSelectedButton();
            if (SFML.Window.Mouse.IsButtonPressed(SFML.Window.Mouse.Button.Left))
                _clicked = true;
            else
            {
                if(_clicked)
                    _nowSelected?.Activate();
                _clicked = false;
            }
        }

        private void HoverSelectedButton()
        {
            Button b = GetSelectedButton();
            if(_nowSelected != b)
            {
                _nowSelected?.Select(false);
                _nowSelected = b;
            }

            b?.Select();
        }

        private Button GetSelectedButton()
        {
            foreach (Button button in _buttons)
                if (TextTools.IsTextPointedByMouse(button.Text))
                    return button;
                    
            return null;
        }

        protected void MoveButtonsToTexts()
        {
            foreach(Button button in _buttons)
                _texts.Add(button.Text);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target?.Draw(_background);
            foreach (Text text in _texts)
                target?.Draw(text);
        }
    }
}
