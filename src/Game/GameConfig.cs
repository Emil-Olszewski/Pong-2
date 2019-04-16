using SFML.System;
using SFML.Graphics;
using Pong_SFML.Configs;

namespace Pong_SFML.Game
{   
    public static class GameConfig
    {
        // Window /
        public static readonly uint W_WIDTH = WindowConfig.WIDTH;
        public static readonly uint W_HEIGHT = WindowConfig.HEIGHT;

        // Player //
        public static readonly Vector2f PLAYER_SIZE = new Vector2f(20, 125);
        public static readonly float PLAYER_MAX_SPEED = 6.0f;
        public static readonly float PLAYER_ACCELERATION = 0.7f;
        public static readonly float PLAYER_BRAKING_FACTOR = 0.85f;
        public static readonly float PLAYER_FROM_X_EDGE_SPAWN = 75;
        public static readonly float PLAYER_Y_SPAWN = (W_HEIGHT / 2) - (PLAYER_SIZE.Y / 2);
        public static readonly float COLOR_CHANGER_MULTIPLIER= 0.05f;
        public static readonly int PLAYER_MAX_ENERGY_POINTS = 3;

        //Ball
        public static readonly float BALL_RADIUS = 10;
        public static readonly float BALL_INIT_SPEED = 65;
        public static readonly Vector2f BALL_SPAWN_POS = new Vector2f(W_WIDTH / 2 - BALL_RADIUS / 2, W_HEIGHT / 2 - BALL_RADIUS / 2);
        public static readonly Color BALL_COLOR = Color.White;
        public static readonly Color ULTRABALL_COLOR = Color.Red;
        public static readonly Color GOLDBALL_COLOR = new Color(255, 215, 0);

        //Interface
        public static readonly uint FONT_SIZE = 72;
        public static readonly float FONT_SIZE_MULTIPLIER = 1.4f;
        public static readonly Vector2f P_ONE_SCORE_POS = new Vector2f(350, 30);
        public static readonly Vector2f P_TWO_SCORE_POS = new Vector2f(W_WIDTH-350-FONT_SIZE, 30);
        public static readonly int ENERGY_RECT_SIZE = 25;
        public static readonly int ENERGY_RECT_SPACE_BETWEEN= 5;
        public static readonly int ENERGY_RECTS_FROM_X_EDGE = 120;
        public static readonly Color ENERGY_RECTS_COLOR = new Color(255, 255, 255);
        public static readonly Color ACTIVE_ENERGY_RECTS_COLOR = new Color(255, 130, 97);
        public static readonly Color INACTIVE_ENERGY_RECTS_COLOR = new Color(60, 60, 60);
        
        //Resources
        public static readonly string FONT_PATH = "7Squared.ttf";

        //Audio
        public static readonly int SPECTRUM_LINES = 128;
        public static readonly int SPECTRUM_REFRESH = 25;
        public static readonly float SPECTRUM_BAR_WIDTH = 8;
        public static readonly float SPECTRUM_BAR_SPACE_BETWEEN = 2.24f;
        public static readonly int SPECTRUM_COLOR_BARS = 15;

        //Game
        public static readonly float BACKGROUND_COLOR_MULTIPLIER = 0.06f;
        public static readonly int WAITING_DURATION = 5;
        public static readonly int MATCH_DURATION = 90;
    }
}
