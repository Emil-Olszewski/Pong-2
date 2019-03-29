using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using Pong_SFML.Game;
using Pong_SFML.Game.Entities;
using Pong_SFML.Game.CollisionSystem;
using System;
using Pong_SFML.Game.Systems;
using Pong_SFML.Game.AudioManagament;
namespace Pong_SFML
{
    public class GameController
    {
        public enum Direction { LEFT, RIGHT, UP, DOWN, NONE };
        GameInterface GameInterface = new GameInterface();

        public void Run()
        {
            Audio.Play();
        }

        public void Update(RenderWindow Win, List<SFML.Window.Keyboard.Key> keysPressed)
        {
            CollisionManager.Update();
            EntitiesManager.Update();
            EntitiesManager.Draw(Win);
            UpdateInterface(Win);
        }

        private void UpdateInterface(RenderWindow Win)
        {
            GameInterface.UpdateScore(0, Entities.Goals[1].Score);
            GameInterface.UpdateScore(1, Entities.Goals[0].Score);
            GameInterface.Update();
            Win.Draw(GameInterface);
            Audio.Update();
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

                case SFML.Window.Keyboard.Key.R:
                    Entities.Ball.UltraBoost = true;
                    break;
            }
        }
    }
}
