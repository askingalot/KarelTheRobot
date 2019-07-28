using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KarelTheRobot.Library.Config;
using KarelTheRobot.Library.Exceptions;

namespace KarelTheRobot.Library
{
    /// <Summary>
    ///  The robot's world is a rectangle surrounded by walls.
    ///  It may contain internal walls and beepers for the robot to interact with.
    ///  You may think it's a lonely place, but, fortunately, most robots enjoy time to themselves.
    /// </Summary>
    public class World
    {
        private int _sleepInterval = 350;
        private bool _isFirstDisplay = true;
        private int _streetCount;
        private int _avenueCount;
        private List<string> _log = new List<string>();
        private Robot _robot;
        private List<Beeper> _beepers;
        private List<Wall> _walls;
        private readonly WorldConfig _config;

        // Note: later elements will be drawn on top of earlier elements
        private IEnumerable<WorldObject> _worldObjects =>
            _beepers
                .Concat<WorldObject>(_walls)
                .Concat(new[] { _robot });

        /// <Summary>
        ///  Make a new world based on a configuration blueprint
        /// </Summary>
        public World(WorldConfig config)
        {
            _config = config;
            _beepers = _config.Beepers;
            _walls = _config.Walls;
            _streetCount = _config.StreetCount;
            _avenueCount = _config.AvenueCount;
        }

        /// <Summary>
        ///  Write a message to the screen. It will appear to the right of the world.
        /// </Summary>
        public void Log(string message)
        {
            _log.Add(message);
        }

        internal void PlaceRobot(Robot robot)
        {
            _robot = robot;
            _robot.SetLocation(_config.RobotStreet, _config.RobotAvenue);
        }

        internal void PlaceBeeper(Beeper beeper)
        {
            if (!_beepers.Contains(beeper))
            {
                _beepers.Add(beeper);
            }
        }

        internal Beeper GetBeeper(int street, int avenue)
        {
            var beeper = _beepers.FirstOrDefault(b => b.Street == street && b.Avenue == avenue);
            if (beeper == null)
            {
                throw new BeeperNotFoundException(street, avenue);
            }
            _beepers.Remove(beeper);
            return beeper;
        }

        internal ObjectType ObjectTypeAt((int street, int avenue) pos) =>
            ObjectTypeAt(pos.street, pos.avenue);

        internal ObjectType ObjectTypeAt(int street, int avenue)
        {
            if (street < 0 || avenue < 0 ||
                street > _streetCount + 1 || avenue > _avenueCount + 1)
            {
                throw new Exception("Location outside the world");
            }
            if (street == 0 || avenue == 0
                || street == _streetCount + 1 || avenue == _avenueCount + 1
                || _walls.Any(w => w.Street == street && w.Avenue == avenue))
            {
                return ObjectType.Wall;
            }

            if (_beepers.Any(b => b.Street == street && b.Avenue == avenue))
            {
                return ObjectType.Beeper;
            }

            return ObjectType.Emptiness;
        }

        internal void Display()
        {
            var newestLog = _log
                .SkipWhile((_, i) => _log.Count - i > _streetCount)
                .ToList();
            var padding = 3;
            var upperLeft = '\u250F';
            var upperRight = '\u2513';
            var lowerLeft = '\u2517';
            var lowerRight = '\u251B';
            var topHorizontalWall = '\u252F';
            var bottomHorizontalWall = '\u2537';
            var leftVerticalWall = '\u2523';
            var rightVerticalWall = '\u252B';

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            if (_isFirstDisplay) {
                Console.Clear();
                _isFirstDisplay = false;
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(_config.ChallengeText);
            Console.WriteLine();
            Console.Write(new string(' ', padding));
            Console.Write(upperLeft);
            Console.Write(new string(topHorizontalWall, _avenueCount));
            Console.Write(upperRight);

            if (newestLog.Any())
            {
                Console.Write($"  --Log--");
            }

            for (var i = 0; i < _streetCount; i++)
            {
                Console.WriteLine();
                Console.Write(new string(' ', padding));
                Console.Write(leftVerticalWall);

                var prevFgColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(new string('\u00B7', _avenueCount));
                Console.ForegroundColor = prevFgColor;
                Console.Write(rightVerticalWall);

                if (i < newestLog.Count)
                {
                    Console.Write($"    {newestLog[i]}");
                }
            }
            Console.WriteLine();
            Console.Write(new string(' ', padding));
            Console.Write(lowerLeft);
            Console.Write(new string(bottomHorizontalWall, _avenueCount));
            Console.Write(lowerRight);
            Console.Write(new string('\n', padding));

            foreach (var wo in _worldObjects)
            {
                Console.SetCursorPosition(
                    wo.Avenue + padding,
                    (_streetCount - wo.Street) + padding);
                Console.Write(wo);
            }

            Console.SetCursorPosition(0, _streetCount + padding * 2);
            Thread.Sleep(_sleepInterval);
        }
    }
}