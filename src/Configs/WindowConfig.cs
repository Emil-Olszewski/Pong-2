namespace Pong_SFML.Configs
{
    public static class WindowConfig
    {
        public static uint Width { get; private set; }
        public static uint Height { get; private set; }
        public static string Title { get; private set; }
        public static uint Framerate { get; private set; }

        static WindowConfig()
        {
            Width = 1024;
            Height = 576;
            Title = "Pong SFML";
            Framerate = 144;
        }
    }
}
