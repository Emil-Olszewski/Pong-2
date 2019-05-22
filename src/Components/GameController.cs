using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using Pong_SFML.Game;
using Pong_SFML.Game.Entities;
using Pong_SFML.Game.Interface;
using Pong_SFML.Game.AudioSystem;
using Pong_SFML.Game.AudioSystem.Types;
using Pong_SFML.Configs;
using Pong_SFML.Components.Controls;

namespace Pong_SFML.Components
{
    public static class GameController
    {
        public enum Direction { LEFT, RIGHT, UP, DOWN, NONE };
        public static Color BackgroundColor = Color.Black;
        public static Song Song;

        private static Counter _startCounter;
        private static Counter _gameCounter;
        private static Counter _backToMenuCounter;

        private static List<FunctionKey> _p1Controls;
        private static List<FunctionKey> _p2Controls;

        public static void Run()
        {
            EntitiesManager.Reset();
            InterfaceManager.Reset();
            BackgroundColor = GameConfig.BACKGROUND_COLOR;
            SetAudioController();
            SetTimers();
            _startCounter.Start();
            SetControls();
        }

        private static void SetAudioController()
        {
            AudioController.LoadMusic(Song.Path);
            AudioController.StartMusic();
        }

        private static void SetTimers()
        {
            _startCounter = new Counter(GameConfig.WAITING_DURATION, 69, "GO!");
            _gameCounter = new Counter(GameConfig.MATCH_DURATION, 54, "STOP!");
            _gameCounter.OnFinish += AudioController.StopMusic;
            _backToMenuCounter = new Counter(GameConfig.SCORE_SHOW_DURATION, 0, "");
            _backToMenuCounter.OnFinish += MainController.RunMenu;

            _startCounter.Reset();
            _gameCounter.Reset();
            _backToMenuCounter.Reset();
        }

        private static void SetControls()
        {
            _p1Controls = new List<FunctionKey>()
            {
                new FunctionKey(Keyboard.Key.W, Entities.Player1.MoveUp),
                new FunctionKey(Keyboard.Key.S, Entities.Player1.MoveDown),
                new FunctionKey(Keyboard.Key.R, Entities.Player1.AddBoost),
                new FunctionKey(Keyboard.Key.T, Entities.Player1.AddTransparent)
            };

            _p2Controls = new List<FunctionKey>()
            {
                new FunctionKey(Keyboard.Key.Up, Entities.Player2.MoveUp),
                new FunctionKey(Keyboard.Key.Down, Entities.Player2.MoveDown),
                new FunctionKey(Keyboard.Key.Numpad1, Entities.Player2.AddBoost),
                new FunctionKey(Keyboard.Key.Numpad2, Entities.Player2.AddTransparent)
            };
        }

    public static void Update()
        {
            MainWindow.Win.Draw(AudioController.Spectrum);
            AudioController.Update();
            RefreshBackground();
            KeyReactor.Do(_p1Controls);
            KeyReactor.Do(_p2Controls);

            if (_startCounter.Active)
                MainWindow.Win.Draw(_startCounter);

            if(_startCounter.Finished && _gameCounter.Active == false && _gameCounter.Finished == false)
                _gameCounter.Start();

            if(_gameCounter.Active)
                UpdateSystems();

            if (_gameCounter.Finished)
            {
                InterfaceManager.EndShow();
                _backToMenuCounter.Start();
            }

            if (_gameCounter.Active || _gameCounter.Finished)
            {
                InterfaceManager.Draw(MainWindow.Win);
                MainWindow.Win.Draw(_gameCounter);
            }

            EntitiesManager.Draw(MainWindow.Win); 
        }

        private static void UpdateSystems()
        {
            EntitiesManager.Update();
            CommunicateWithInterface();
        }

        private static void RefreshBackground() =>
            BackgroundColor = new Color((byte)(AudioController.Spectrum.Color.R * GameConfig.BACKGROUND_COLOR_MULTIPLIER), 
                (byte)(AudioController.Spectrum.Color.G * GameConfig.BACKGROUND_COLOR_MULTIPLIER), 
                (byte)(AudioController.Spectrum.Color.B * GameConfig.BACKGROUND_COLOR_MULTIPLIER));

        private static void CommunicateWithInterface()
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

    }
}
