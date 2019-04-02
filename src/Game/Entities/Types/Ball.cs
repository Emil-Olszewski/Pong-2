using System;
using SFML.Graphics;
using SFML.System;

namespace Pong_SFML.Game.Entities.Types
{
    public class Ball : MovableEntity
    {
        public override bool IsCollidable { get; protected set; }
        public override Shape Body { get; protected set; }
        public bool UltraBoost { get; set; }

        public Ball()
        {
            Body = new CircleShape()
            {
                Position = GameConfig.BALL_SPAWN_POS,
                Radius = GameConfig.BALL_RADIUS,
                FillColor = Color.White
            };

            IsCollidable = true;
            Velocity = new Vector2f(5, 0.8f);
            PreviousVelocity = new Vector2f();
            BounceFactor = 1.0f;
            SetID();
            UltraBoost = false;
        }

        public override void UpdatePosition()
        {
            Move();
            if(UltraBoost)
                Move();
        }

        public override void Update()
        {
            UpdatePosition();
        }

        public override void WasHit()
        {
            UltraBoost = false;
        }
    }
}
