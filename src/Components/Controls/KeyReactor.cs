using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong_SFML.Components.Controls
{
    static class KeyReactor
    {
        public static void Do(List<List<FunctionKey>> functionKeysLists)
        {
            foreach (List<FunctionKey> list in functionKeysLists)
                Do(list);
        }

        public static void Do(List<FunctionKey> functionKeys)
        {
            foreach (FunctionKey key in functionKeys)
            {
                if (KeysInfo.Holded.Contains(key.Key))
                    key.Do();
                else if(KeysInfo.Pressed.Contains(key.Key))
                {
                    KeysInfo.Pressed.Remove(key.Key);
                    key.Do();
                }
            }     
        }
    }
}
