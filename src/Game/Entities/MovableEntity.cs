using System.Collections.Generic;
using SFML.System;

namespace Pong_SFML.Game.Entities
{
    public abstract class MovableEntity : Entity
    {
        public Vector2f Velocity { get; set; }
        public Vector2f PreviousVelocity { get; set; }
        public List<Bonus.Type> ActiveBonuses;
        public float BounceFactor { get; set; }

        public abstract void UpdatePosition();

        public abstract void ResetPosition();

        public abstract void Init();

        public override void Reset() => Init();

        protected virtual void Move() 
            => Body.Position = new Vector2f(Body.Position.X + Velocity.X, Body.Position.Y + Velocity.Y);
    }
}
