using System;
using System.Linq;
using System.Threading;

namespace KarelTheRobot
{
    public class World
    {
        private int _streetCount = 10;
        private int _avenueCount = 20;

        private Robot _robot;

        public void PlaceRobot(Robot robot)
        {
            _robot = robot;
        }

        public ObjectType ObjectTypeAt((int street, int avenue) pos) =>
            ObjectTypeAt(pos.street, pos.avenue);

        public ObjectType ObjectTypeAt(int street, int avenue)
        {
            if (street == 0 || avenue == 0 ||
                street == _streetCount + 1 || avenue == _avenueCount + 1)
            {
                return ObjectType.Wall;
            }
            if (street < 0 || avenue < 0 ||
                street > _streetCount + 1 || avenue > _avenueCount + 1)
            {
                throw new Exception("Location outside the world");
            }

            return ObjectType.Emptiness;
        }

        public void Display()
        {
            int padding = 3;
            char upperLeft = '\u250F';
            char upperRight = '\u2513';
            char lowerLeft = '\u2517';
            char lowerRight = '\u251B';
            char horizontalWall = '\u2501';
            char verticalWall = '\u2503';

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write(new string(' ', padding));
            Console.Write(upperLeft);
            Console.Write(new string(horizontalWall, _avenueCount));
            Console.Write(upperRight);
            for (int i = 0; i < _streetCount; i++)
            {
                Console.WriteLine();
                Console.Write(new string(' ', padding));
                Console.Write(verticalWall);
                Console.Write(new string(' ', _avenueCount));
                Console.Write(verticalWall);
            }
            Console.WriteLine();
            Console.Write(new string(' ', padding));
            Console.Write(lowerLeft);
            Console.Write(new string(horizontalWall, _avenueCount));
            Console.Write(lowerRight);
            Console.Write(new string('\n', padding));

            Console.SetCursorPosition(
                _robot.Avenue + padding,
                (_streetCount - _robot.Street) + padding);

            Console.Write(_robot);

            Console.SetCursorPosition(0, _streetCount + padding * 2);
            Thread.Sleep(500);
        }
    }
}