using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Pong_SFML.Game.Entities.Types
{
    public class Ball : MovableEntity
    {
        public override bool IsCollidable { get; protected set; }
        public override Shape Body { get; protected set; }
        private int _boost;
        private int _goldboost;
        private int _transparent;
        private List<CircleShape> _trails = new List<CircleShape>();
        private int _trailsLimit = 80;
        private int _drawnTrails = 10;

        public Ball()
        {
            Body = new CircleShape()
            {
                Radius = GameConfig.BALL_RADIUS,
                FillColor = Color.White
            };

            ResetPosition();

            IsCollidable = true;
            Velocity = GetRandomVector(GameConfig.BALL_INIT_SPEED);
            PreviousVelocity = new Vector2f();
            BounceFactor = 1.001f;
            SetID();
            ActiveBonuses = new List<Bonus.Type>();
        }

        private Vector2f GetRandomVector(float speed)
        {
            Random rand = new Random();
            float X = rand.Next(1, (int)Math.Sqrt(speed));
            float Y = (float)Math.Sqrt(speed - Math.Pow(X, 2));
            return new Vector2f(X, Y);
        }

        public override void UpdatePosition()
        {
            Move();
            if (_goldboost > 0)
                for (int i = 0; i < 3; i++)
                    Move();
            else if (_boost > 0)
            {
                AudioSystem.AudioController.PlaySound("RIKOCHET");
                Move();
            }
        }

        public override void Update()
        {
            UpdatePosition();
            ApplyBonusses();
            ResetPositionIfOutsideMap();
        }

        private void ResetPositionIfOutsideMap()
        {
            if (Body.Position.X > GameConfig.W_WIDTH + 10 || Body.Position.X < -10
                || Body.Position.Y < -10 || Body.Position.Y > GameConfig.W_HEIGHT + 10)
            {
                ResetPosition();
                float actualSpeed = (float)(Velocity.X * Velocity.X + Velocity.Y * Velocity.Y);
                Velocity = GetRandomVector(actualSpeed);
            }
        }

        protected override void Move()
        {
            base.Move();
            _trails.Add(new CircleShape()
            {
                Radius = GameConfig.BALL_RADIUS,
                FillColor = new Color(Body.FillColor.R, Body.FillColor.G, Body.FillColor.B, 3),
                Position = Body.Position
            });

            ReduceTrails();
        }

        private void ReduceTrails()
        {
            while (_trails.Count > _trailsLimit)
                _trails.RemoveAt(0);
        }

        private void ApplyBonusses()
        {
            foreach(Bonus.Type type in ActiveBonuses)
            {
                switch(type)
                {
                    case Bonus.Type.BOOST:
                        _boost++;
                        Body.FillColor = GameConfig.ULTRABALL_COLOR;
                        _drawnTrails = 25;
                        break;

                    case Bonus.Type.TRANSPARENT:
                        _transparent++;
                        MakeTransparent();
                        break;
                }
            }

            if(_boost >= 4)
            {
                Body.FillColor = GameConfig.GOLDBALL_COLOR;
                _drawnTrails = 80;
                _goldboost = 5;
                _boost = 0;
                AudioSystem.AudioController.PlaySound("EXPLOSION");
            }

            ActiveBonuses.Clear();
        }

        private void MakeTransparent() => Body.FillColor = new Color(Body.FillColor.R, Body.FillColor.G, Body.FillColor.B, 0);

        public override void WasHit()
        {
            if (_boost > 0)
                _boost--;

            if (_goldboost > 0)
                _goldboost--;

            if (_boost == 0 && _goldboost == 0)
            {
                _drawnTrails = 10;
                Body.FillColor = new Color(GameConfig.BALL_COLOR.R, GameConfig.BALL_COLOR.G, GameConfig.BALL_COLOR.B, Body.FillColor.A);
            }

            if (_transparent > 0)
            {
                _transparent--;
                if (_transparent == 0)
                    Body.FillColor = new Color(Body.FillColor.R, Body.FillColor.G, Body.FillColor.B, 255);
            }
        }

        public void WasHitWith(Player player)
        {
            foreach (Bonus.Type bonus in player.ActiveBonuses)
                ActiveBonuses.Add(bonus);
            player.ActiveBonuses.Clear();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);

            if(_trails.Count > 0)
            for (int i = _trails.Count - 1, j = 0; j < _drawnTrails; i--, j++)
            {
                if (_trails.Count > j)
                    target.Draw(_trails[i]);
                else
                    break;
            }
        }

        public override void ResetPosition() => Body.Position = GameConfig.BALL_SPAWN_POS;
    }
}
