using SFML.System;
using Pong_SFML.Configs;

namespace Pong_SFML.Game
{
    public static class GameConfig
    {
        // Player //
        public static readonly Vector2f PlayerSize = new Vector2f(20, 100);
        public static readonly float PlayerMaxSpeed = 4.0f;
        public static readonly float PlayerAcceleration = 0.7f;
        public static readonly float PlayerBrakingFactor = 0.85f;
        public static readonly float XFromEdgeSpawnForPlayer = 75;
        public static readonly float YSpawnForPlayer = (WindowConfig.Height / 2) - (PlayerSize.Y / 2);
        public static readonly float ColorChangerMultiplier = 0.05f;

        //Ball
        public static readonly float BallRadius = 10;
        public static readonly Vector2f BallSpawnPos = new Vector2f(WindowConfig.Width / 2 - BallRadius / 2, WindowConfig.Height / 2 - BallRadius / 2);

    }
}
