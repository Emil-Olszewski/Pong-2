using System.Collections.Generic;
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
                X = GameConfig.PLAYER_FROM_X_EDGE_SPAWN,
                Y = GameConfig.PLAYER_Y_SPAWN - GameConfig.PLAYER_SIZE.Y/2
            });

            Player2 = new Player(new Vector2f
            {
                X = GameConfig.W_WIDTH - GameConfig.PLAYER_FROM_X_EDGE_SPAWN - GameConfig.PLAYER_SIZE.X,
                Y = GameConfig.PLAYER_Y_SPAWN - GameConfig.PLAYER_SIZE.Y / 2
            });

            Ball = new Ball();

            Walls = new List<Wall>
            {
                new Wall(new Vector2f(1260, 10), new Vector2f(10, 10), Color.White),
                new Wall(new Vector2f(1260, 10), new Vector2f(10, 700), Color.White),
            };

            Goals = new List<Goal>
            {
                new Goal(new Vector2f(10, 700), new Vector2f(10, 10), Color.White),
                new Goal(new Vector2f(10, 700), new Vector2f(1260, 10), Color.White),
            };     
        }

        static public Player GetPlayer(int ID)
        {
            if (Player1.ID == ID) return Player1;
            else return Player2;
        }
    }
}
