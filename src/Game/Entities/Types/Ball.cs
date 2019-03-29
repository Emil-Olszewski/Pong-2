﻿using System;
using SFML.Graphics;
using SFML.System;

namespace Pong_SFML.Game.Entities.Types
{
    public class Ball : MovableEntity
    {
        public override bool IsCollidable { get; protected set; }
        public override Shape Body { get; protected set; }

        public Ball()
        {
            Body = new CircleShape()
            {
                Position = GameConfig.BallSpawnPos,
                Radius = GameConfig.BallRadius,
                FillColor = Color.White
            };

            IsCollidable = true;
            Velocity = new Vector2f(5, 0.8f);
            PreviousVelocity = new Vector2f();
            BounceFactor = 1.0f;
            SetID();
        }

        public override void UpdatePosition()
        {
            Move();
        }

        public override void Update()
        {
            UpdatePosition();
        }
    }
}