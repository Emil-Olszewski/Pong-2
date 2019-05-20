using System;
using SFML.Graphics;

namespace Pong_SFML.Menu.Components
{
    public class Button
    {
        public Text Text = new Text();
        private Color _originColor;
        private bool _selected = false;

        public void Select(bool state = true)
        {
            if (state && !_selected)
            {
                _originColor = new Color(Text.FillColor.R, Text.FillColor.G, Text.FillColor.B, Text.FillColor.A);
                Text.FillColor = new Color(94, 126, 171);
                _selected = true;
            }
            else if(!state)
            {
                Text.FillColor = _originColor;
                _selected = false;
            }
        }

        public void Activate() => OnButtonClicked(new EventArgs());

        public event EventHandler<EventArgs> ButtonClicked;
        protected virtual void OnButtonClicked(EventArgs e) => ButtonClicked?.Invoke(this, e);
    }
}
