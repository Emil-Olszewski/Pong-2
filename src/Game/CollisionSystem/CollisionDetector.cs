using Pong_SFML.Game.CollisionSystem;
using Pong_SFML.Game.Entities;
using SFML.Graphics;
using SFML.System;
using System;

namespace Pong_SFML.Game.CollisionSystem
{
    public static class CollisionDetector
    {
        public static bool WillBeColliding(MovableEntity mov, Entity ent)
        {
            FloatRect rect = mov.GetFloatRect();
            rect.Left += mov.Velocity.X;
            rect.Top += mov.Velocity.Y;

            if (ent.GetFloatRect().Intersects(rect))
                return true;
            else
                return false;
        }

        public static Vector2f DistanceToCollision(FloatRect rect1, FloatRect rect2)
        {
            Vector2f distance = new Vector2f();

            if(rect1.Left > rect2.Left)
                distance.X = -(rect1.Left - rect2.Left - rect2.Width);
            else
                distance.X = rect2.Left - rect1.Left - rect1.Width;

            if (rect1.Top > rect2.Top)
                distance.Y = -(rect1.Top - rect2.Top - rect2.Height);
            else
                distance.Y = rect2.Top - rect1.Top - rect1.Height;

            return distance;
        }

        public static bool IsSticking(FloatRect rect1, FloatRect rect2)
        {
            if (rect2.Intersects(rect1)) return true;

            rect1.Left += 0.1f;
            rect1.Top += 0.1f;

            if (rect2.Intersects(rect1)) return true;

            rect1.Left -= 0.2f;
            rect1.Top -= 0.2f;

            if (rect2.Intersects(rect1)) return true;
            else return false; 
        }
    }
}
