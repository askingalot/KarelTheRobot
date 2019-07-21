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

        public void Display()
        {
            int padding = 3;
            int borderSize = 1;
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

            Console.SetCursorPosition(_robot.Avenue + borderSize + padding,
                                      _robot.Street + borderSize + padding);
            Console.Write(_robot);

            Console.SetCursorPosition(0, _streetCount + padding * 2);
            Thread.Sleep(500);
        }
    }
}