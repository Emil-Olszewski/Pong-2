using System.Collections.Generic;
using SFML.Graphics;
using Pong_SFML.Game.Entities;
using Pong_SFML.Game.CollisionSystem;
using Pong_SFML.Game.Systems;
using Pong_SFML.Game.AudioSystem;

namespace Pong_SFML
{
    public class GameController
    {
        public enum Direction { LEFT, RIGHT, UP, DOWN, NONE };
        public Color BackgroundColor = Color.Black;
        GameInterface GameInterface = new GameInterface();

        public void Run()
        {
            AudioController.InitBass("test.mp3");
            AudioController.Play();
        }

        public void Update(RenderWindow Win, List<SFML.Window.Keyboard.Key> keysPressed)
        {
            AudioController.Update();
            Win.Draw(AudioController.Spectrum);

            CollisionManager.Update();
            EntitiesManager.Update();
            EntitiesManager.Draw(Win);
            UpdateInterface(Win);
            UpdateBackground();
        }

        private void UpdateBackground()
        {
            BackgroundColor = new Color((byte)(AudioController.Spectrum.Color.R / 10), 
                (byte)(AudioController.Spectrum.Color.R / 10), (byte)(AudioController.Spectrum.Color.R / 10));
        }

        private void UpdateInterface(RenderWindow Win)
        {
            GameInterface.UpdateScore(0, Entities.Goals[1].Score);
            GameInterface.UpdateScore(1, Entities.Goals[0].Score);
            GameInterface.Update();
            Win.Draw(GameInterface);
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
