using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
using Pong_SFML.Game.Entities;
using Pong_SFML.Game.Interface;
using Pong_SFML.Game.AudioSystem;
using Pong_SFML.Game.AudioSystem.Types;
using Pong_SFML.Components;
using Pong_SFML.Components.KeyboardHandle;

namespace Pong_SFML.Game
{
    public static class GameController
    {
        public enum Direction { LEFT, RIGHT, UP, DOWN, NONE };
        public static Color BackgroundColor;
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
            MainWindow.BackgroundColor = BackgroundColor;
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
                new FunctionKey(Keyboard.Key.W, EntitiesContainer.Player1.MoveUp, true),
                new FunctionKey(Keyboard.Key.S, EntitiesContainer.Player1.MoveDown, true),
                new FunctionKey(Keyboard.Key.R, EntitiesContainer.Player1.AddBoost),
                new FunctionKey(Keyboard.Key.T, EntitiesContainer.Player1.AddTransparent)
            };

            _p2Controls = new List<FunctionKey>()
            {
                new FunctionKey(Keyboard.Key.Up, EntitiesContainer.Player2.MoveUp, true),
                new FunctionKey(Keyboard.Key.Down, EntitiesContainer.Player2.MoveDown, true),
                new FunctionKey(Keyboard.Key.Numpad1, EntitiesContainer.Player2.AddBoost),
                new FunctionKey(Keyboard.Key.Numpad2, EntitiesContainer.Player2.AddTransparent)
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
                EntitiesContainer.Goals[1].Score, EntitiesContainer.Goals[0].Score
            });

            InterfaceManager.SetEnergyPoints(new List<int>()
            {
                 EntitiesContainer.Player1.EnergyPoints,  EntitiesContainer.Player2.EnergyPoints
            });

            EntitiesContainer.Player1.UpdateScore(EntitiesContainer.Goals[1].Score);
            EntitiesContainer.Player2.UpdateScore(EntitiesContainer.Goals[0].Score);
        }
    }
}
