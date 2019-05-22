using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;

namespace Pong_SFML.Components.Controls
{
    class FunctionKey
    {
        public Keyboard.Key Key { get; set; }
        private event EventHandler<EventArgs> Pressed;

        protected virtual void OnPressed(EventArgs e)
            => Pressed?.Invoke(this, e);

        public void Do()
            => OnPressed(EventArgs.Empty);

        public FunctionKey(Keyboard.Key key, EventHandler<EventArgs> pressed)
        {
            Key = key;
            Pressed += pressed;
        }
    }
}
