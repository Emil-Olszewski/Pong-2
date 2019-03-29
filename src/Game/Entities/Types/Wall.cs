using SFML.Graphics;
using SFML.System;
using System;
using Pong_SFML.Game.Systems;

namespace Pong_SFML.Game.Entities.Types
{
    public class Wall : Entity
    {
        public override bool IsCollidable { get; protected set; }
        public override Shape Body { get; protected set; }
        ColorChanger ColorChanger = new ColorChanger();
        public Wall(Vector2f size, Vector2f pos, Color color)
        {
            Body = new RectangleShape
            {
                Size = size,
                Position = pos,
                FillColor = color
            };

            IsCollidable = true;
            SetID();
        }

        public override void Update()
        {
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
