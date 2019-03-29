using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace Pong_SFML.Game.Entities
{
    public abstract class MovableEntity : Entity
    {
        public Vector2f Velocity { get; set; }
        public Vector2f PreviousVelocity { get; set; }
        public float BounceFactor { get; set; }
        public abstract void UpdatePosition();
        protected virtual void Move() => Body.Position = new Vector2f(Body.Position.X + Velocity.X, Body.Position.Y + Velocity.Y);
    }
}
