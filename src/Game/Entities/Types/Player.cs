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
        private int _previousPoints;
        public int Points { get; set; }
        public int EnergyPoints { get; private set; }
        private bool _isMoving;

        ColorChanger ColorChanger = new ColorChanger();

        public Player(Vector2f pos)
        {
            Body = new RectangleShape(pos)
            {
                Position = pos,
                FillColor = Color.Magenta,
                Size = GameConfig.PLAYER_SIZE     
            };

            Velocity = new Vector2f();
            PreviousVelocity = new Vector2f();
            IsCollidable = true;
            SetID();  
        }

        public override void UpdatePosition()
        {
            Move();
            IfBodyIsOutsideMapBringItBack();
            Brake();
            _isMoving = false;
        }

        public void AddVelocity(GameController.Direction dir)
        {
            _isMoving = true;
            switch (dir)
            {
                case GameController.Direction.UP:
                    SetVelocity(new Vector2f(Velocity.X, Velocity.Y - GameConfig.PLAYER_ACCELERATION));
                    break;

                case GameController.Direction.DOWN:
                    SetVelocity(new Vector2f(Velocity.X, Velocity.Y + GameConfig.PLAYER_ACCELERATION));
                    break;
            }
        }

        public void SetVelocity(Vector2f value)
        {           
            if (value.Y > GameConfig.PLAYER_MAX_SPEED)
                Velocity = new Vector2f(Velocity.X, GameConfig.PLAYER_MAX_SPEED);
            else if (value.Y < -GameConfig.PLAYER_MAX_SPEED)
                Velocity = new Vector2f(Velocity.X, -GameConfig.PLAYER_MAX_SPEED);
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
            if(_isMoving == false)
            {
                Velocity = new Vector2f(Math.Abs(Velocity.X) > GameConfig.COLOR_CHANGER_MULTIPLIER? Velocity.X * GameConfig.PLAYER_BRAKING_FACTOR : 0, Velocity.Y);
                Velocity = new Vector2f(Velocity.X, Math.Abs(Velocity.Y) > GameConfig.COLOR_CHANGER_MULTIPLIER? Velocity.Y * GameConfig.PLAYER_BRAKING_FACTOR : 0);
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

        public void UpdateScore(int score)
        {
            _previousPoints = Points;
            Points = score;
            if (Points - _previousPoints > 0)
                AddEnergyPoint();
        }

        private void AddEnergyPoint()
        {
            EnergyPoints++;
            if (EnergyPoints > GameConfig.PLAYER_MAX_ENERGY_POINTS)
                EnergyPoints = GameConfig.PLAYER_MAX_ENERGY_POINTS;
        }
    }
}
