using System.Collections.Generic;
using SFML.Graphics;

namespace Pong_SFML.Game.Interface
{
    public static class InterfaceManager
    {
        private static Scores _scores = new Scores();
        private static EnergyRectangles _energyRectangles = new EnergyRectangles();
        private static bool _endShow = false;
        public static void SetScores(List<int> scores) => _scores.Update(scores);
        public static void SetEnergyPoints(List<int> points) => _energyRectangles.Update(points);

        public static void EndShow()
        {
            _endShow = true;
            _scores.EndShow();
        }

        public static void Draw(RenderWindow win)
        {
            if(_endShow == false)
                win.Draw(_energyRectangles);
            win.Draw(_scores);
        }

        public static void Reset()
        {
            _scores.Reset();
            _endShow = false;
        }
    }
}
