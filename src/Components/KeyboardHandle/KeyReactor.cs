using System.Collections.Generic;

namespace Pong_SFML.Components.KeyboardHandle
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
                if (key.Hold && KeysInfo.Holded.Contains(key.Key))
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
