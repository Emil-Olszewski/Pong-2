using System.Collections.Generic;
using SFML.Window;

namespace Pong_SFML.Components.Controls
{
    public static class KeysInfo
    {
        public static List<Keyboard.Key> Pressed = new List<Keyboard.Key>();
        public static List<Keyboard.Key> Holded = new List<Keyboard.Key>();
    }
}
