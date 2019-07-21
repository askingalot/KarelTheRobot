using System;

namespace KarelTheRobot
{
    public class RobotDestructionException : Exception {
        public RobotDestructionException(string message) 
            : base($"Robot Destroyed! ({message})") { }
    }

    public class RobotWallCollisionException : RobotDestructionException {
        public RobotWallCollisionException() : base("Hit a wall") { }
    }

    public class Robot
    {
        // Run East - West
        private int _street = 1;
        public int Street => _street;

        // Run North - South
        private int _avenue = 1;
        public int Avenue => _avenue;

        private Direction _direction = Direction.East;
        private readonly World _world;

        public Robot(World world)
        {
            _world = world;
            world.PlaceRobot(this);
            _world.Display();
        }

        public void Move()
        {
            var (street, avenue) = (_street, _avenue);
            switch (_direction)
            {
                case Direction.North:
                    street++;
                    break;
                case Direction.South:
                    street--;
                    break;
                case Direction.East:
                    avenue++;
                    break;
                case Direction.West:
                    avenue--;
                    break;
            }

            (_street, _avenue) = (street, avenue);
            _world.Display();

            if (_world.ObjectTypeAt(street, avenue) == ObjectType.Wall) {
                throw new RobotWallCollisionException();
            }
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
            _world.Display();
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