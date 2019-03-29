using System;
using SFML.Graphics;
using SFML.System;
using Pong_SFML.Game.Systems;

namespace Pong_SFML.Game.Entities.Types
{
    public class Player : MovableEntity
    {
        public override bool IsCollidable { get; protected set; }
        public override Shape Body { get; protected set; }
        private bool isMoving;

        ColorChanger ColorChanger = new ColorChanger();

        public Player(Vector2f pos)
        {
            Body = new RectangleShape(pos)
            {
                Position = pos,
                FillColor = Color.Magenta,
                Size = GameConfig.PlayerSize     
            };

            Velocity = new Vector2f();
            PreviousVelocity = new Vector2f();
            BounceFactor = 0f;
            IsCollidable = true;
            SetID();  
        }

        public override void UpdatePosition()
        {
            Move();
            IfBodyIsOutsideMapBringItBack();
            Brake();
            isMoving = false;
        }

        public void AddVelocity(GameController.Direction dir)
        {
            isMoving = true;
            switch (dir)
            {
                case GameController.Direction.UP:
                    SetVelocity(new Vector2f(Velocity.X, Velocity.Y - GameConfig.PlayerAcceleration));
                    break;

                case GameController.Direction.DOWN:
                    SetVelocity(new Vector2f(Velocity.X, Velocity.Y + GameConfig.PlayerAcceleration));
                    break;
            }
        }

        public void SetVelocity(Vector2f value)
        {           
            if (value.Y > GameConfig.PlayerMaxSpeed)
                Velocity = new Vector2f(Velocity.X, GameConfig.PlayerMaxSpeed);
            else if (value.Y < -GameConfig.PlayerMaxSpeed)
                Velocity = new Vector2f(Velocity.X, -GameConfig.PlayerMaxSpeed);
            else Velocity = value;
        }

        public void IfBodyIsOutsideMapBringItBack()
        {
            if (Body.Position.Y + Body.GetGlobalBounds().Height < 0)
                Body.Position = new Vector2f(Body.Position.X, 576);
            if (Body.Position.Y > 576)
                Body.Position = new Vector2f(Body.Position.X, 0 - Body.GetGlobalBounds().Height);
        }

        private void Brake()
        {
            if(isMoving == false)
            {
                Velocity = new Vector2f(Math.Abs(Velocity.X) > GameConfig.ColorChangerMultiplier ? Velocity.X * GameConfig.PlayerBrakingFactor : 0, Velocity.Y);
                Velocity = new Vector2f(Velocity.X, Math.Abs(Velocity.Y) > GameConfig.ColorChangerMultiplier ? Velocity.Y * GameConfig.PlayerBrakingFactor : 0);
            }
        }

        public override void Update()
        {
            UpdatePosition();
            Body.FillColor = ColorChanger.Get(Body.FillColor);
        }

        public override void WasHit()
        {
            Random rand = new Random();
            Color randomColor = new Color((byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), 255);
            ColorChanger.CountActualColorDifference(Body.FillColor, randomColor);
        }
    }
}
