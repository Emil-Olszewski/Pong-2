using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using Pong_SFML.Components;
using Pong_SFML.Game.Systems;

namespace Pong_SFML.Game.Entities.Types
{
    public class Player : MovableEntity
    {
        public override bool IsCollidable { get; protected set; }
        public override Shape Body { get; protected set; }
        public int Points { get; set; }
        public int EnergyPoints { get; private set; }
        private int _previousPoints;
        private bool _isMoving;
        private bool _isPlayerOne;

        ColorChanger ColorChanger = new ColorChanger();

        public Player(bool isPlayerOne)
        {
            _isPlayerOne = isPlayerOne;
            Init();
        }

        public override void Init()
        {
            Body = new RectangleShape()
            {
                FillColor = GameConfig.PLAYER_COLOR,
                Size = GameConfig.PLAYER_SIZE,
                Position = _isPlayerOne ? GameConfig.PLAYER1_POS : GameConfig.PLAYER2_POS
            };

            Velocity = new Vector2f(0,0);
            PreviousVelocity = new Vector2f(0,0);
            ActiveBonuses = new List<Bonus.Type>();
            IsCollidable = true;
            SetID();
        }

        public override void UpdatePosition()
        {
            Move();
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

        public void MoveUp(object sender, EventArgs e)
            => AddVelocity(GameController.Direction.UP);

        public void MoveDown(object sender, EventArgs e)
            => AddVelocity(GameController.Direction.DOWN);

        public void SetVelocity(Vector2f value)
        {           
            if (value.Y > GameConfig.PLAYER_MAX_SPEED)
                Velocity = new Vector2f(Velocity.X, GameConfig.PLAYER_MAX_SPEED);
            else if (value.Y < -GameConfig.PLAYER_MAX_SPEED)
                Velocity = new Vector2f(Velocity.X, -GameConfig.PLAYER_MAX_SPEED);
            else Velocity = value;
        }

        private void Brake()
        {
            if(_isMoving == false)
            {
                Velocity = new Vector2f(Math.Abs(Velocity.X) > GameConfig.COLOR_CHANGER_MULTIPLIER 
                    ? Velocity.X * GameConfig.PLAYER_BRAKING_FACTOR : 0, Velocity.Y);
                Velocity = new Vector2f(Velocity.X, Math.Abs(Velocity.Y) > GameConfig.COLOR_CHANGER_MULTIPLIER
                    ? Velocity.Y * GameConfig.PLAYER_BRAKING_FACTOR : 0);
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

        public void AddBonus(Bonus.Type bonus)
        {
            if(EnergyPoints >= Bonus.Price(bonus))
            {
                AudioSystem.AudioController.PlaySound("POWER");
                ActiveBonuses.Add(bonus);
                EnergyPoints -= Bonus.Price(bonus);
            }
        }

        public void AddBoost(object sender, EventArgs e)
            => AddBonus(Bonus.Type.BOOST);

        public void AddTransparent(object sender, EventArgs e)
            => AddBonus(Bonus.Type.TRANSPARENT);

        public override void ResetPosition() 
            => Body.Position = new Vector2f(Body.Position.X, GameConfig.PLAYER_Y_SPAWN);
    }
}
