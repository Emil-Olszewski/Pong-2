using Pong_SFML.Game.Entities;
using SFML.Window;
using Pong_SFML.Configs;

namespace Pong_SFML.Game
{
    public static class KeyReactor
    {
        public static void Do(DescrKey dkey)
        {
            switch (dkey.Name)
            {
                case "P1_UP":
                    Entities.Entities.Player1.AddVelocity(GameController.Direction.UP);
                    break;

                case "P1_DOWN":
                    Entities.Entities.Player1.AddVelocity(GameController.Direction.DOWN);
                    break;

                case "P2_UP":
                    Entities.Entities.Player2.AddVelocity(GameController.Direction.UP);
                    break;

                case "P2_DOWN":
                    Entities.Entities.Player2.AddVelocity(GameController.Direction.DOWN);
                    break;

                case "P1_UB":
                    Entities.Entities.Player1.AddBonus(Bonus.Type.BOOST);
                    break;

                case "P2_UB":
                    Entities.Entities.Player2.AddBonus(Bonus.Type.BOOST);
                    break;

                case "P1_TB":
                    Entities.Entities.Player1.AddBonus(Bonus.Type.TRANSPARENT);
                    break;

                case "P2_TB":
                    Entities.Entities.Player2.AddBonus(Bonus.Type.TRANSPARENT);
                    break;
            }
        }
    }
}
