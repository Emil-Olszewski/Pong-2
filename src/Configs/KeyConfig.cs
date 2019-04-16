using System.Collections.Generic;
using SFML.Window;

namespace Pong_SFML.Configs
{
    public static class KeyConfig
    {
        public static List<DescrKey> HoldingKeys;
        public static List<DescrKey> PressingKeys;
       
        static KeyConfig()
        {
            HoldingKeys = new List<DescrKey>
            {
                new DescrKey(Keyboard.Key.W, "P1_UP"),          //player 1 move up
                new DescrKey(Keyboard.Key.S, "P1_DOWN"),        //player 1 move down
                new DescrKey(Keyboard.Key.Up, "P2_UP"),         //player 2 move up
                new DescrKey(Keyboard.Key.Down, "P2_DOWN"),     //player 2 move down
            };

            PressingKeys = new List<DescrKey>
            {
                new DescrKey(Keyboard.Key.Escape, "CLOSE"),     //close
                new DescrKey(Keyboard.Key.R, "P1_UB"),          //player 1 ultra ball
                new DescrKey(Keyboard.Key.Numpad1, "P2_UB"),    //player 2 ultra ball
                new DescrKey(Keyboard.Key.T, "P1_TB"),          //player 1 transparent ball
                new DescrKey(Keyboard.Key.Numpad2, "P2_TB"),    //player 2 transparent ball
            };
        }
    }
}
