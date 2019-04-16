using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Pong_SFML.Game.Interface
{
    public class Scores : Transformable, Drawable
    {
        private Text _ghost;
        private Font _font;
        private List<Text> _playersScores;
        private int _difference;

        public Scores() => InitializeTexts();

        private void InitializeTexts()
        {
            _font = new Font(GameConfig.FONT_PATH);
            _playersScores = new List<Text>()
            {
                new Text("0", _font, GameConfig.FONT_SIZE)
                {
                    Position = GameConfig.P_ONE_SCORE_POS,
                    FillColor = Color.White
                },

                new Text("0", _font, GameConfig.FONT_SIZE)
                {
                    Position = GameConfig.P_TWO_SCORE_POS,
                    FillColor = Color.White
                }
        };

            _ghost = new Text();
        }

        public void Update(List<int> scores)
        {
            Animate();
            for (int i = 0; i < scores.Count; i++)
                SetScore(i, scores[i]);
        }

        private void Animate()
        {
            foreach (Text score in _playersScores)
                if (score.CharacterSize != GameConfig.FONT_SIZE)
                {
                    score.CharacterSize--;
                    if (_ghost.FillColor.A > 20)
                        _ghost.FillColor = new Color(_ghost.FillColor.R, _ghost.FillColor.G, _ghost.FillColor.B,
                            (byte)(_ghost.FillColor.A - _difference));
                    else
                        _ghost.FillColor = new Color(_ghost.FillColor.R, _ghost.FillColor.G, _ghost.FillColor.B, 0);
                }
        }

        private void SetScore(int playerNumber, int score)
        {
            if (_playersScores[playerNumber].DisplayedString != score.ToString())
            {
                _playersScores[playerNumber].CharacterSize = (uint)(GameConfig.FONT_SIZE * GameConfig.FONT_SIZE_MULTIPLIER);
                _playersScores[playerNumber].DisplayedString = score.ToString();
                _ghost = new Text(score.ToString(), _font, _playersScores[playerNumber].CharacterSize)
                {
                    Position = _playersScores[playerNumber].Position,
                    FillColor = new Color(157, 158, 160, 255)
                };

                _difference = (int)(255 / (96 - GameConfig.FONT_SIZE) * 1.5);
            }
        }

        public void EndShow()
        {
            foreach(Text score in _playersScores)
            {
                score.CharacterSize = 96;
                score.Position = new Vector2f(score.Position.X, GameConfig.W_HEIGHT / 2 - score.GetGlobalBounds().Height / 2);
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_ghost);
            foreach (Text score in _playersScores)
                target.Draw(score);
        }
    }
}
