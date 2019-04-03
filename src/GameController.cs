using System.Collections.Generic;
using SFML.Graphics;
using Pong_SFML.Game.Entities;
using Pong_SFML.Game.CollisionSystem;
using Pong_SFML.Game.AudioSystem;
using Pong_SFML.Game.Interface;

namespace Pong_SFML
{
    public class GameController
    {
        public enum Direction { LEFT, RIGHT, UP, DOWN, NONE };
        public Color BackgroundColor = Color.Black;

        public void Run()
        {
            AudioController.InitBass("test2.mp3");
            AudioController.Play();
        }

        public void Update(RenderWindow Win, List<SFML.Window.Keyboard.Key> keysPressed)
        {
            AudioController.Update();
            Win.Draw(AudioController.Spectrum);

            CollisionManager.Update();
            EntitiesManager.Update();
            EntitiesManager.Draw(Win);

            UpdateInterface();
            InterfaceManager.Draw(Win);

            UpdateBackground();
        }

        private void UpdateBackground() =>
            BackgroundColor = new Color((byte)(AudioController.Spectrum.Color.R / 10), 
                (byte)(AudioController.Spectrum.Color.G / 10), (byte)(AudioController.Spectrum.Color.B / 10));

        private void UpdateInterface()
        {
            InterfaceManager.SetScores(new List<int>()
            {
                Entities.Goals[1].Score, Entities.Goals[0].Score
            });

            InterfaceManager.SetEnergyPoints(new List<int>()
            {
                 Entities.Player1.EnergyPoints,  Entities.Player2.EnergyPoints
            });

            Entities.Player1.UpdateScore(Entities.Goals[1].Score);
            Entities.Player2.UpdateScore(Entities.Goals[0].Score);
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
