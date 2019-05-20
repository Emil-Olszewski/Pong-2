using System.Timers;
using Pong_SFML.Configs;
using SFML.Graphics;
using SFML.System;

namespace Pong_SFML.Game.Interface
{
    public class Counter : Transformable, Drawable
    {
        public bool Active { get; set; }
        public bool Finished { get; set; }
        int _seconds;
        Text _text;
        Timer _timer;
        readonly string _message;

        public delegate void FinishedFunction();
        public FinishedFunction OnFinish;

        public Counter(int seconds, int size, string finishMessage)
        {
            _seconds = seconds;
            _message = finishMessage;
            _text = new Text(seconds.ToString(), Fonts.InGameFont)
            {
                FillColor = Color.White,
                CharacterSize = (uint)size
            };

            _text.Position = new Vector2f(GetDisplayMidPos(), GameConfig.P_ONE_SCORE_POS.Y);

            _timer = new Timer(1000);
            _timer.Elapsed += DecreaseSeconds;
            _timer.AutoReset = true;
        }

        private float GetDisplayMidPos()
            => GameConfig.W_WIDTH / 2 - _text.GetLocalBounds().Width / 2;

        public void Start()
        {
            _timer.Enabled = true;
            Active = true;
            Finished = false;
        }

        private void DecreaseSeconds(object sender, ElapsedEventArgs e)
        {
            _seconds--;
            if (_seconds > 0)
                _text.DisplayedString = _seconds.ToString();
            else if (_seconds == 0)
                _text.DisplayedString = _message;
            else
                Finish();

            _text.Position = new Vector2f(GetDisplayMidPos(), GameConfig.P_ONE_SCORE_POS.Y);
        }

        private void Finish()
        {
            _timer.Enabled = false;
            Active = false;
            Finished = true;

            OnFinish?.Invoke();
        }

        public void Reset()
        {
            _timer.Enabled = false;
            Active = false;
            Finished = false;
        }

        public void Draw(RenderTarget target, RenderStates states) => target.Draw(_text);
    }
}
