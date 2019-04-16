using System;
using System.Collections.Generic;
using SFML.Graphics;
using Pong_SFML.Game.Entities;
using Pong_SFML.Game.AudioSystem;
using Pong_SFML.Game.Interface;
using Pong_SFML.Configs;
using Pong_SFML.Game;

namespace Pong_SFML
{
    public class GameController
    {
        public enum Direction { LEFT, RIGHT, UP, DOWN, NONE };
        public Color BackgroundColor = Color.Black;

        private Counter _startCounter;
        private Counter _gameCounter;
        
        public void Run(string musicName)
        {
            SetAudioController(musicName);
            SetTimers();
            _startCounter.Start();
        }

        private static void SetAudioController(string musicName)
        {
            AudioController.LoadMusic("Resources/music/" + musicName + ".mp3");
            AudioController.StartPlayingMusic();
        }

        private void SetTimers()
        {
            _startCounter = new Counter(GameConfig.WAITING_DURATION, 69, "GO!");
            _gameCounter = new Counter(GameConfig.MATCH_DURATION, 54, "STOP!");
        }

        public void Update(RenderWindow Win)
        {
            Win.Draw(AudioController.Spectrum);
            AudioController.Update();
            RefreshBackground();

            if (_startCounter.Active)
                Win.Draw(_startCounter);

            if(_startCounter.Finished && _gameCounter.Active == false && _gameCounter.Finished == false)
                _gameCounter.Start();

            if(_gameCounter.Active)
                UpdateSystems();

            if (_gameCounter.Finished)
                InterfaceManager.EndShow();

            if (_gameCounter.Active || _gameCounter.Finished)
            {
                InterfaceManager.Draw(Win);
                Win.Draw(_gameCounter);
            }

            EntitiesManager.Draw(Win); 
        }

        private void UpdateSystems()
        {
            EntitiesManager.Update();
            CommunicateWithInterface();
        }

        private void RefreshBackground() =>
            BackgroundColor = new Color((byte)(AudioController.Spectrum.Color.R * GameConfig.BACKGROUND_COLOR_MULTIPLIER), 
                (byte)(AudioController.Spectrum.Color.G * GameConfig.BACKGROUND_COLOR_MULTIPLIER), 
                (byte)(AudioController.Spectrum.Color.B * GameConfig.BACKGROUND_COLOR_MULTIPLIER));

        private void CommunicateWithInterface()
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

        public void ReactTo(DescrKey key) => KeyReactor.Do(key);
    }
}
