using System;
using System.Collections.Generic;
using SFML.System;
using Pong_SFML.Game.Entities;
using SFML.Graphics;

namespace Pong_SFML.Game.CollisionSystem
{
    public static class CollisionManager
    {
        public static List<Entity> Collidables;
        public static List<MovableEntity> DynamicCollidables;

        static CollisionManager()
        {
            Collidables = new List<Entity>();
            DynamicCollidables = new List<MovableEntity>();
        }

        public static void Update()
        {
            foreach (MovableEntity mov in DynamicCollidables)
            {
                foreach (Entity ent in Collidables)
                {
                    if (mov == ent || (mov.Velocity.X == 0 && mov.Velocity.Y == 0))
                        continue;

                    if (CollisionDetector.WillBeColliding(mov, ent))
                    {
                        TeleportToCollidingObjectAndSetVelocityTo0(mov, ent);
                        ent.wasHit = true;
                    }
                        

                    if(mov.BounceFactor > 0)
                        BoundFrom(mov, ent);
                }
            }
        }

        private static void BoundFrom(MovableEntity mov, Entity ent)
        {
            bool replay = false;
            FloatRect rect = mov.GetFloatRect();
            while (CollisionDetector.IsSticking(rect, ent.GetFloatRect()))
            {
                float angle = (float)(Math.PI - Math.Atan2(mov.PreviousVelocity.Y, mov.PreviousVelocity.X) * 2);
                if (mov.GetType() == Entities.Entities.Ball.GetType() && ent.GetType() == Entities.Entities.Player1.GetType())
                    angle = GetValidAngleForBallAndPlayer(mov, ent);
                mov.Velocity = GetBoundVector(mov, angle, replay);
                rect.Left = mov.GetFloatRect().Left + mov.Velocity.X;
                rect.Top = mov.GetFloatRect().Top + mov.Velocity.Y;
                replay = true;
            }

    }

    private static void TeleportToCollidingObjectAndSetVelocityTo0(MovableEntity mov, Entity ent)
        {
            Vector2f dis = CollisionDetector.DistanceToCollision(mov.GetFloatRect(), ent.GetFloatRect());
            mov.PreviousVelocity = mov.Velocity;

            if (Math.Abs(dis.X) > Math.Abs(dis.Y))
            {
                if (mov.Velocity.Y != 0)
                { 
                    mov.Body.Position = new Vector2f(mov.Body.Position.X, mov.Body.Position.Y + dis.Y);
                    mov.Velocity = new Vector2f(mov.Velocity.X, 0);
                }
            }
            else
            {
                if (mov.Velocity.X != 0)
                {
                    mov.Body.Position = new Vector2f(mov.Body.Position.X + dis.X, mov.Body.Position.Y);
                    mov.Velocity = new Vector2f(0, mov.Velocity.Y);
                }
            }
        }

        private static Vector2f GetBoundVector(MovableEntity mov, double angle, bool replay)
        {
            Vector2f newVelocity = new Vector2f
            {
                X = mov.BounceFactor * (mov.PreviousVelocity.X * (float)Math.Cos(angle) - mov.PreviousVelocity.Y * (float)Math.Sin(angle)),
                Y = mov.BounceFactor * (mov.PreviousVelocity.X * (float)Math.Sin(angle) + mov.PreviousVelocity.Y * (float)Math.Cos(angle))
            };

            if (replay)
                newVelocity = -newVelocity;

            return newVelocity;
        }

        private static float GetValidAngleForBallAndPlayer(MovableEntity ball, Entity player)
        {
            float lenghtOfArea = player.GetFloatRect().Height / 8;
            float middleOfBall = ball.GetFloatRect().Top + ball.GetFloatRect().Height / 2;
            float touchingArea = (middleOfBall - player.GetFloatRect().Top) / lenghtOfArea;

            float bounceAngle;
            switch((int)touchingArea)
            {
                case 4:
                case 5:
                    bounceAngle = 45;
                    break;

                case 3:
                case 6:
                    bounceAngle = 45;
                    break;

                case 2:
                case 7:
                    bounceAngle = 45;
                    break;

                case 1:
                case 8:
                default:
                    bounceAngle = 45;
                    break;
            }

            return (float)((Math.PI * bounceAngle) / 180);
        }
    }
}
