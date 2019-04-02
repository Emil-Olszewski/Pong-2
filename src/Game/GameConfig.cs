using SFML.System;
using Pong_SFML.Configs;

namespace Pong_SFML.Game
{
    public static class GameConfig
    {
        // Player //
        public static readonly Vector2f PLAYER_SIZE = new Vector2f(20, 100);
        public static readonly float PLAYER_MAX_SPEED = 4.0f;
        public static readonly float PLAYER_ACCELERATION = 0.7f;
        public static readonly float PLAYER_BRAKING_FACTOR = 0.85f;
        public static readonly float PLAYER_FROM_X_EDGE_SPAWN = 75;
        public static readonly float PLAYER_Y_SPAWN = (WindowConfig.Height / 2) - (PLAYER_SIZE.Y / 2);
        public static readonly float COLOR_CHANGER_MULTIPLIER= 0.05f;

        //Ball
        public static readonly float BALL_RADIUS = 10;
        public static readonly Vector2f BALL_SPAWN_POS = new Vector2f(WindowConfig.Width / 2 - BALL_RADIUS / 2, WindowConfig.Height / 2 - BALL_RADIUS / 2);

        //Interface
        public static readonly uint FONT_SIZE = 72;
        public static readonly Vector2f P_ONE_SCORE_POS = new Vector2f(350, 30);
        public static readonly Vector2f P_TWO_SCORE_POS = new Vector2f(WindowConfig.Width-350-FONT_SIZE, 30);

        //Resources
        public static readonly string FONT_PATH = "7Squared.ttf";

        //Audio
        public static readonly int SPECTRUM_LINES = 128;
        public static readonly int SPECTRUM_REFRESH = 25;
        public static readonly float SPECTRUM_BAR_WIDTH = 8;
        public static readonly float SPECTRUM_BAR_SPACE_BETWEEN = 2.24f;
        public static readonly int SPECTRUM_COLOR_BAR = 15;
    }
}
