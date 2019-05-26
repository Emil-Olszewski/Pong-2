using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using Pong_SFML.Configs;
using Pong_SFML.Game.Entities.Types;

namespace Pong_SFML.Game.Entities
{
    public static class EntitiesContainer
    {
        public static Player Player1; 
        public static Player Player2;
        public static Ball Ball;
        public static List<Wall> Walls;
        public static List<Goal> Goals;

        static EntitiesContainer() => Create();

        private static void Create()
        {
            Player1 = new Player(true);

            Player2 = new Player(false);

            Ball = new Ball();

            Walls = new List<Wall>
            {
                new Wall(new Vector2f(WindowConfig.WIDTH-20, 10), new Vector2f(10, 10), Color.White),
                new Wall(new Vector2f(WindowConfig.WIDTH-20, 10), new Vector2f(10, WindowConfig.HEIGHT-20), Color.White),
            };

            Goals = new List<Goal>
            {
                new Goal(new Vector2f(10, WindowConfig.HEIGHT-20), new Vector2f(10, 10), Color.White),
                new Goal(new Vector2f(10, WindowConfig.HEIGHT-20), new Vector2f(WindowConfig.WIDTH-20, 10), Color.White),
            };
        }

        public static Player GetPlayer(int ID)
        {
            if (Player1.ID == ID) return Player1;
            else return Player2;
        }
    }
}
