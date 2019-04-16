namespace Pong_SFML.Game.Entities
{
    static public class Bonus
    {
        public enum Type { BOOST, TRANSPARENT };

        static public int Price(Type type)
        {
            switch(type)
            {
                case Type.BOOST:
                    return 1;

                case Type.TRANSPARENT:
                    return 1;

                default:
                    return 1;
            }
        }
    }
}
