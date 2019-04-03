using System.Collections.Generic;
using SFML.Graphics;

namespace Pong_SFML.Game.Interface
{
    public static class InterfaceManager
    {
        private static Scores _scores = new Scores();
        private static EnergyRectangles _energyRectangles = new EnergyRectangles();

        public static void SetScores(List<int> scores) => _scores.Update(scores);
        public static void SetEnergyPoints(List<int> points) => _energyRectangles.Update(points);

        public static void Draw(RenderWindow win)
        {
            win.Draw(_energyRectangles);
            win.Draw(_scores);
        }
    }
}
