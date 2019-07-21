using System;
using KarelTheRobot.Exceptions;

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

        private Direction _direction = Direction.East;
        private readonly World _world;

        public Robot(World world)
        {
            _world = world;
            world.PlaceRobot(this);
            _world.Display();
        }

        public bool FrontIsClear =>
            _world.ObjectTypeAt(PositionAt(_direction)) != ObjectType.Wall;

        public bool LeftIsClear =>
            _world.ObjectTypeAt(PositionAt(LeftDirection)) != ObjectType.Wall;

        public bool RightIsClear =>
            _world.ObjectTypeAt(PositionAt(RightDirection)) != ObjectType.Wall;

        public void Move()
        {
            (_street, _avenue) = PositionAt(_direction);
            _world.Display();

            if (_world.ObjectTypeAt(_street, _avenue) == ObjectType.Wall)
            {
                throw new RobotWallCollisionException();
            }
        }

        public void TurnLeft()
        {
            _direction = LeftDirection;
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

        private (int street, int avenue) PositionAt(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return (_street + 1, _avenue);
                case Direction.South:
                    return (_street - 1, _avenue);
                case Direction.East:
                    return (_street, _avenue + 1);
                case Direction.West:
                    return (_street, _avenue - 1);
                default:
                    throw new Exception("Invalid Direction");
            }
        }

        private Direction RightDirection
        {
            get
            {
                switch (_direction)
                {
                    case Direction.North:
                        return Direction.East;
                    case Direction.South:
                        return Direction.West;
                    case Direction.East:
                        return Direction.South;
                    case Direction.West:
                        return Direction.North;
                    default:
                        throw new Exception("Invalid Direction");
                }
            }
        }

        private Direction LeftDirection
        {
            get
            {
                switch (_direction)
                {
                    case Direction.North:
                        return Direction.West;
                    case Direction.South:
                        return Direction.East;
                    case Direction.East:
                        return Direction.North;
                    case Direction.West:
                        return Direction.South;
                    default:
                        throw new Exception("Invalid Direction");
                }
            }
        }
    }
}