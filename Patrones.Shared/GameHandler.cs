using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Patrones.Shared
{
    public sealed class GameHandler
    {

        public static GameHandler Instance { get { return Nested.instance; } }

        private class Nested
        {

            static Nested()
            {
            }

            internal static readonly GameHandler instance = new GameHandler();

        }

        private Stopwatch _watch;
        private Random _random;
        private double _currentTime;

        public double StepsPerSecond = 120;

        private int _lastTime = 0;
        private int _lastTimer = 0; //to output the ticks and fps in the console
        private double _unprocessed = 0; //counts unprocessed ticks to compensate
        private double _msPerTick;
        private int _frames = 0;
        private int _ticks = 0;

        public List<Vector2> Positions;

        private GameHandler()
        {
            _watch = new Stopwatch();
            _random = new Random();
            Positions = new List<Vector2>();
        }

        public void Update(GameTime gameTime)
        {
            if (!_watch.IsRunning)
            {
                _watch.Restart();
            }
            _msPerTick = 1000 / StepsPerSecond;
            _currentTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            _currentTime = _watch.ElapsedMilliseconds;
            int now = (int)_currentTime;
            _unprocessed += (now - _lastTime) / _msPerTick; //calculates unprocessed time
            _lastTime = now;
            bool shouldRender = true;

            while (_unprocessed >= 1)
            {
                _ticks++;
                Step();
                _unprocessed -= 1;
                shouldRender = true;
            }

            if (shouldRender)
            {
                _frames++;
                //Draw();
            }

            if (_currentTime - _lastTimer > 1000)
            {
                _lastTimer += 1000;
                Console.WriteLine(_ticks + " ticks, " + _frames + " f/s");
                _frames = 0;
                _ticks = 0;
            }

        }

        public void Step()
        {
            int i = Positions.Count - 1;
            while (i >= 0)
            {
                var position = Positions[i];
                position.Y += 1;
                Positions[i] = position;
                i--;
            }
            Positions.Add(RandomPosition());
        }

        private Vector2 RandomPosition()
        {
            return new Vector2(_random.Next(0, 600), _random.Next(0, 600));
        }

    }
}
