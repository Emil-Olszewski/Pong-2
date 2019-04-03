using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Pong_SFML.Game.Interface
{
    public class EnergyRectangles : Transformable, Drawable
    {
        private List<List<RectangleShape>> _playersEnergyRectangles;

        public EnergyRectangles() => InitializeRectangles();

        private void InitializeRectangles()
        {
            _playersEnergyRectangles = new List<List<RectangleShape>>();

            for(int i = 0; i < 2; i++)
            {
                _playersEnergyRectangles.Add(new List<RectangleShape>());
                for (int j = 0; j < GameConfig.PLAYER_MAX_ENERGY_POINTS; j++)
                {
                    _playersEnergyRectangles[i].Add(new RectangleShape()
                    {
                        Size = new Vector2f(GameConfig.ENERGY_RECT_SIZE, GameConfig.ENERGY_RECT_SIZE),
                        FillColor = GameConfig.INACTIVE_ENERGY_RECTS_COLOR,
                        Position = new Vector2f(GameConfig.ENERGY_RECTS_FROM_X_EDGE + j * (GameConfig.ENERGY_RECT_SPACE_BETWEEN + GameConfig.ENERGY_RECT_SIZE),
                        GameConfig.P_ONE_SCORE_POS.Y + GameConfig.FONT_SIZE / 2 - GameConfig.ENERGY_RECT_SIZE / 2)
                    });

                    if (i == 1)
                        _playersEnergyRectangles[i][j].Position = new Vector2f(Configs.WindowConfig.Width - GameConfig.ENERGY_RECTS_FROM_X_EDGE - j * (GameConfig.ENERGY_RECT_SPACE_BETWEEN + GameConfig.ENERGY_RECT_SIZE),
                            GameConfig.P_ONE_SCORE_POS.Y + GameConfig.FONT_SIZE / 2 - GameConfig.ENERGY_RECT_SIZE / 2);
                }
            }        
        }
        public void Update(List<int> points)
        {
            for(int i = 0; i < points.Count; i++)
                SetRectangles(i, points[i]);
        }

        private void SetRectangles(int player, int points)
        {
            for (int i = 0; i < GameConfig.PLAYER_MAX_ENERGY_POINTS; i++)
            {
                if (points >= i + 1)
                    _playersEnergyRectangles[player][i] = ChangeVisibility(_playersEnergyRectangles[player][i], true);
                else
                    _playersEnergyRectangles[player][i] = ChangeVisibility(_playersEnergyRectangles[player][i], false);
            }
        }

        private RectangleShape ChangeVisibility(RectangleShape rect, bool state)
        {
            if (state)
                rect.FillColor = GameConfig.ENERGY_RECTS_COLOR;
            else
                rect.FillColor = GameConfig.INACTIVE_ENERGY_RECTS_COLOR;
            return rect;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (List<RectangleShape> list in _playersEnergyRectangles)
                foreach (RectangleShape rect in list)
                    target.Draw(rect);
        }
    }
}
