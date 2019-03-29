using System;
using System.Collections.Generic;
using Pong_SFML.Configs;
using Pong_SFML.Game.Entities.Types;
using SFML.Graphics;
using SFML.System;

namespace Pong_SFML.Game.Entities
{
    public static class Entities
    {
        public static Player Player1; 
        public static Player Player2;
        public static Ball Ball;
        public static List<Wall> Walls;
        public static List<Goal> Goals;

        static Entities()
        {
            Player1 = new Player(new Vector2f
            {
                X = GameConfig.XFromEdgeSpawnForPlayer,
                Y = GameConfig.YSpawnForPlayer - GameConfig.PlayerSize.Y/2
            });

            Player2 = new Player(new Vector2f
            {
                X = WindowConfig.Width - GameConfig.XFromEdgeSpawnForPlayer - GameConfig.PlayerSize.X,
                Y = GameConfig.YSpawnForPlayer - GameConfig.PlayerSize.Y / 2
            });

            Ball = new Ball();

            Walls = new List<Wall>
            {
                new Wall(new Vector2f(1004, 10), new Vector2f(10, 10), Color.White),
                new Wall(new Vector2f(1004, 10), new Vector2f(10, 556), Color.White),
            };

            Goals = new List<Goal>
            {
                new Goal(new Vector2f(10, 556), new Vector2f(10, 10), Color.White),
                new Goal(new Vector2f(10, 556), new Vector2f(1004, 10), Color.White),
            };

        }
    }
}
