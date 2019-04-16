using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using Pong_SFML.Game.Entities;

namespace Pong_SFML.Game.CollisionSystem
{
    public static class CollisionManager
    {
        public static List<Entity> Collidables = new List<Entity>();
        public static List<MovableEntity> DynamicCollidables = new List<MovableEntity>();

        public static void Update()
        {
            foreach (MovableEntity mov in DynamicCollidables)
            {
                foreach (Entity ent in Collidables)
                {
                    if (mov == ent || mov.Velocity == new Vector2f(0,0))
                        continue;

                    if (CollisionDetector.WillBeColliding(mov, ent))
                    {
                        TeleportToCollidingObjectAndSetVelocityTo0(mov, ent);
                        ent.WasHit();
                        mov.WasHit();
                    }

                    if(mov.BounceFactor > 0)
                        BoundFrom(mov, ent);
                }
            }
        }

        private static void BoundFrom(MovableEntity mov, Entity ent)
        {
            int replay = 0;
            FloatRect rect = mov.GetFloatRect();
            while (CollisionDetector.IsSticking(rect, ent.GetFloatRect()))
            {
                float angle = (float)(Math.PI - Math.Atan2(mov.PreviousVelocity.Y, mov.PreviousVelocity.X) * 2);
                if (mov.GetType() == Entities.Entities.Ball.GetType() && ent.GetType() == Entities.Entities.Player1.GetType())
                {
                    angle = GetValidAngleForBallAndPlayer(mov, ent);
                    Entities.Entities.Ball.WasHitWith(Entities.Entities.GetPlayer(ent.ID));
                    AudioSystem.AudioController.PlaySound("HIT");
                }
                mov.Velocity = GetBoundVector(mov, angle, replay > 0 ? true : false);
                rect.Left = mov.GetFloatRect().Left + mov.Velocity.X;
                rect.Top = mov.GetFloatRect().Top + mov.Velocity.Y;
                replay++;

                if (replay > 3)
                    mov.ResetPosition();
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
                    if (mov.Velocity.X != 0)
                        mov.Velocity = new Vector2f((float)Math.Sqrt(mov.Velocity.X * mov.Velocity.X + mov.Velocity.Y * mov.Velocity.Y), 0);
                    else
                        mov.Velocity = new Vector2f(mov.Velocity.X, 0);
                }
            }
            else
            {
                if (mov.Velocity.X != 0)
                {
                    mov.Body.Position = new Vector2f(mov.Body.Position.X + dis.X, mov.Body.Position.Y);
                    if (mov.Velocity.Y != 0)
                        mov.Velocity = new Vector2f(0, (float)Math.Sqrt(mov.Velocity.X * mov.Velocity.X + mov.Velocity.Y * mov.Velocity.Y));
                    else
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

            if(Math.Abs(Math.Atan2(newVelocity.Y, newVelocity.X)) > 1.13 && Math.Abs(Math.Atan2(newVelocity.Y, newVelocity.X)) < 2.00)
                newVelocity = IncreaseAngle(newVelocity);

            return newVelocity;
        }

        private static Vector2f IncreaseAngle(Vector2f newVelocity)
        {
            double vectorValue = Math.Pow(newVelocity.X, 2) + Math.Pow(newVelocity.Y, 2);

            while (Math.Abs(Math.Atan2(newVelocity.Y, newVelocity.X)) > 1.13 && Math.Abs(Math.Atan2(newVelocity.Y, newVelocity.X)) < 2.00)
                if (newVelocity.X != 0)
                    newVelocity.X *= 1.1f;
                else
                    newVelocity.X = 1;

            float newY = (float)Math.Sqrt(vectorValue - Math.Pow(newVelocity.X, 2));
            if (newVelocity.Y < 0)
                newY = -newY;

            newVelocity.Y = newY;
            return newVelocity;
        }

        private static float GetValidAngleForBallAndPlayer(MovableEntity ball, Entity player)
        {
            float lenghtOfArea = player.GetFloatRect().Height * 1.3f / 8;
            float middleOfBall = ball.GetFloatRect().Top + ball.GetFloatRect().Height / 2;
            float touchingArea = (middleOfBall - player.GetFloatRect().Top) / lenghtOfArea;

            float bounceAngle;
            switch((int)touchingArea)
            {
                case 1:
                    bounceAngle = 15;
                    break;
                case 2:
                    bounceAngle = 45;
                    break;
                case 3:
                    bounceAngle = 75;
                    break;
                case 4:
                    bounceAngle = 105;
                    break;
                case 5:
                    bounceAngle = -105;
                    break;
                case 6:
                    bounceAngle = -75;
                    break;
                case 7:
                    bounceAngle = -45;
                    break;
                case 8:
                    bounceAngle = -15;
                    break;
                default:
                    bounceAngle = 90;
                    break;
            }

            return (float)((Math.PI * bounceAngle) / 180);
        }
    }
}
