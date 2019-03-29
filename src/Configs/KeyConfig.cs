using System.Collections.Generic;
using SFML.Window;

namespace Pong_SFML.Configs
{
    public static class KeyConfig
    {
        public static List<SFML.Window.Keyboard.Key> KeysInUse;
       
        static KeyConfig()
        {
            KeysInUse = new List<SFML.Window.Keyboard.Key>
            {
                Keyboard.Key.W,     // W            player 1 move up
                Keyboard.Key.S,     // S            player 1 move down
                Keyboard.Key.Up,    // Up Arrow     player 2 move up
                Keyboard.Key.Down   // Down arrow   player 2 move down
            };
        }
    }
}
