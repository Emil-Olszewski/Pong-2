using System.Collections.Generic;
using SFML.Window;

namespace Pong_SFML.Configs
{
    public static class KeysInUse
    {
        public static List<Keyboard.Key> Holding = new List<Keyboard.Key>
        {
            Keyboard.Key.W, Keyboard.Key.S,
            Keyboard.Key.Up, Keyboard.Key.Down
        };

        public static List<Keyboard.Key> Pressing = new List<Keyboard.Key>
        {
            Keyboard.Key.Escape, Keyboard.Key.R, Keyboard.Key.T,
            Keyboard.Key.Numpad1, Keyboard.Key.Numpad2
        };

    }
}
