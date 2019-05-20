namespace Pong_SFML.Game.AudioSystem.Types
{
    public class Song
    {
        public string Path;
        public string Name;
        public string DisplayName;

        public Song(string path, string name, string dname)
        {
            Path = path;
            Name = name;
            DisplayName = dname;
        }
    }
}
