using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KarelTheRobot.Exceptions;

namespace KarelTheRobot
{
    public class World
    {
        private int _streetCount = 10;
        private int _avenueCount = 20;
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

        public World(WorldConfig config)
        {
            _config = config;
            _beepers = _config.Beepers;
            _walls = _config.Walls;
        }

        public void PlaceRobot(Robot robot)
        {
            _robot = robot;
        }

        public void PlaceBeeper(Beeper beeper)
        {
            if (!_beepers.Contains(beeper))
            {
                _beepers.Add(beeper);
            }
        }

        public Beeper GetBeeper(int street, int avenue)
        {
            var beeper = _beepers.FirstOrDefault(b => b.Street == street && b.Avenue == avenue);
            if (beeper == null)
            {
                throw new BeeperNotFoundException(street, avenue);
            }
            _beepers.Remove(beeper);
            return beeper;
        }

        public ObjectType ObjectTypeAt((int street, int avenue) pos) =>
            ObjectTypeAt(pos.street, pos.avenue);

        public ObjectType ObjectTypeAt(int street, int avenue)
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

        public void Log(string message)
        {
            _log.Add(message);
        }

        public void Display()
        {
            int padding = 3;
            char upperLeft = '\u250F';
            char upperRight = '\u2513';
            char lowerLeft = '\u2517';
            char lowerRight = '\u251B';
            char topHorizontalWall = '\u252F';
            char bottomHorizontalWall = '\u2537';
            char leftVerticalWall = '\u2523';
            char rightVerticalWall = '\u252B';

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write(new string(' ', padding));
            Console.Write(upperLeft);
            Console.Write(new string(topHorizontalWall, _avenueCount));
            Console.Write(upperRight);
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
            Console.WriteLine("---------------------------");
            foreach (var msg in _log)
            {
                Console.WriteLine(msg);
            }
            Console.WriteLine("---------------------------");
            Thread.Sleep(500);
        }
    }
}