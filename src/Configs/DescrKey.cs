using SFML.Window;

namespace Pong_SFML.Configs
{
    public class DescrKey
    {
        public Keyboard.Key Key { get; private set; }
        public string Name { get; private set; }

        public DescrKey(Keyboard.Key key, string name)
        {
            Key = key;
            Name = name;
        }
    }
}
