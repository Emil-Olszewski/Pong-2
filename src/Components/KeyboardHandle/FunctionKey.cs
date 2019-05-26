using System;
using SFML.Window;

namespace Pong_SFML.Components.KeyboardHandle
{
    class FunctionKey
    {
        public Keyboard.Key Key { get; set; }
        private event EventHandler<EventArgs> Pressed;
        public bool Hold;

        protected virtual void OnPressed(EventArgs e)
            => Pressed?.Invoke(this, e);

        public void Do()
            => OnPressed(EventArgs.Empty);

        public FunctionKey(Keyboard.Key key, EventHandler<EventArgs> pressed, bool hold = false)
        {
            Key = key;
            Pressed += pressed;
            Hold = hold;
        }
    }
}
