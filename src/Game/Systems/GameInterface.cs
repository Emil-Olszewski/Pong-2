using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Pong_SFML.Game.Systems
{
    public class GameInterface : Transformable, Drawable
    {
        private Font Font;
        private List<Text> PlayersScores;
        public Text Ghost { get; private set; }
        private int difference;

        public GameInterface()
        {
            Font = new Font(GameConfig.FontPath);
            PlayersScores = new List<Text>()
            {
                new Text("0", Font, GameConfig.FontSize)
                {
                    Position = GameConfig.PlayerOneScorePosition,
                    FillColor = Color.White
                },

                new Text("0", Font, GameConfig.FontSize)
                {
                    Position = GameConfig.PlayerTwoScorePosition,
                    FillColor = Color.White
                }
        };
          
            Ghost = new Text();
        }

        public void Update()
        {
            foreach(Text score in PlayersScores)
                if(score.CharacterSize != GameConfig.FontSize)
                {
                    score.CharacterSize--;
                    if(Ghost.FillColor.A > 20)
                        Ghost.FillColor = new Color(Ghost.FillColor.R, Ghost.FillColor.G, Ghost.FillColor.B, (byte)(Ghost.FillColor.A - difference));
                    else
                        Ghost.FillColor = new Color(Ghost.FillColor.R, Ghost.FillColor.G, Ghost.FillColor.B, 0);
                }
        }

        public void UpdateScore(int playerNumber, int score)
        {
            if(PlayersScores[playerNumber].DisplayedString != score.ToString())
            {
                PlayersScores[playerNumber].CharacterSize = 96;
                PlayersScores[playerNumber].DisplayedString = score.ToString();
                Ghost = new Text(score.ToString(), Font, PlayersScores[playerNumber].CharacterSize)
                {
                    Position = PlayersScores[playerNumber].Position,
                    FillColor = new Color(157, 158, 160, 255)
                };

                difference = (int)(255 / (96 - GameConfig.FontSize) * 1.5);
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Ghost);
            foreach (Text score in PlayersScores)
                target.Draw(score);
        }
    }
}
