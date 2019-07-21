using System;

namespace KarelTheRobot
{
    public class Robot
    {
        // Run East - West
        private int _street = 1;
        public int Street => _street;

        // Run North - South
        private int _avenue = 1;
        public int Avenue => _avenue;

        private Direction _direction = Direction.North;
        private readonly World _world;

        public Robot(World world)
        {
            _world = world;
            world.PlaceRobot(this);
            _world.Display();
        }

        public void Move()
        {
            switch (_direction)
            {
                case Direction.North:
                    _street--;
                    break;
                case Direction.South:
                    _street++;
                    break;
                case Direction.East:
                    _avenue++;
                    break;
                case Direction.West:
                    _avenue--;
                    break;
            }
            _world.Display();
        }

        public void TurnLeft()
        {
            switch (_direction)
            {
                case Direction.North:
                    _direction = Direction.West;
                    break;
                case Direction.South:
                    _direction = Direction.East;
                    break;
                case Direction.East:
                    _direction = Direction.North;
                    break;
                case Direction.West:
                    _direction = Direction.South;
                    break;
            }
        }

        public override string ToString()
        {
            switch (_direction)
            {
                case Direction.North:
                    return "^";
                case Direction.South:
                    return "v";
                case Direction.East:
                    return ">";
                case Direction.West:
                    return "<";
                default:
                    throw new Exception("Invalid Direction");
            }
        }
    }
}