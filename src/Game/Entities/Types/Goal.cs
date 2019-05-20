using SFML.System;
using SFML.Graphics;

namespace Pong_SFML.Game.Entities.Types
{
    public class Goal : Wall
    {
        public int Score { get; set; }

        public Goal(Vector2f size, Vector2f pos, Color color) : base(size, pos, color)
            => Init();

        private void Init() => Score = 0;

        public override void WasHit()
        {
            base.WasHit();
            AudioSystem.AudioController.PlaySound("SCORE");
            Score++;
        }

        public override void Reset()
        {
            base.Reset();
            Init();
        }
    }
}
