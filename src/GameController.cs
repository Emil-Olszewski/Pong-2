using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using Pong_SFML.Game;
using Pong_SFML.Game.Entities;
using Pong_SFML.Game.CollisionSystem;
using System;

namespace Pong_SFML
{
    public class GameController
    {
        public enum Direction { LEFT, RIGHT, UP, DOWN, NONE };

        public void Run()
        {
    
        }

        public void Update(RenderWindow Win, List<SFML.Window.Keyboard.Key> keysPressed)
        {
            CollisionManager.Update();
            EntitiesManager.Update();
            EntitiesManager.Draw(Win);
        }

        public void ReactTo(SFML.Window.Keyboard.Key key)
        {
            switch (key)
            {
                case SFML.Window.Keyboard.Key.W:
                    Entities.Player1.AddVelocity(Direction.UP);
                    break;

                case SFML.Window.Keyboard.Key.S:
                    Entities.Player1.AddVelocity(Direction.DOWN);
                    break;

                case SFML.Window.Keyboard.Key.Up:
                    Entities.Player2.AddVelocity(Direction.UP);
                    break;

                case SFML.Window.Keyboard.Key.Down:
                    Entities.Player2.AddVelocity(Direction.DOWN);
                    break;
            }
        }
    }
}
