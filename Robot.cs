using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly List<Beeper> _bag = new List<Beeper>();

        public Robot(World world)
        {
            _world = world;
            world.PlaceRobot(this);
            _world.Display();
        }

        public bool IsFrontClear =>
            _world.ObjectTypeAt(PositionAt(_direction)) != ObjectType.Wall;
        public bool IsLeftClear =>
            _world.ObjectTypeAt(PositionAt(LeftDirection)) != ObjectType.Wall;
        public bool IsRightClear =>
            _world.ObjectTypeAt(PositionAt(RightDirection)) != ObjectType.Wall;
        public bool IsNextToBeeper =>
            _world.ObjectTypeAt(_street, _avenue) == ObjectType.Beeper;

        public bool IsFacingNorth => IsFacing(Direction.North);
        public bool IsFacingSouth => IsFacing(Direction.South);
        public bool IsFacingEast => IsFacing(Direction.East);
        public bool IsFacingWest => IsFacing(Direction.West);

        public void PickBeeper()
        {
            try
            {
                var beeper = _world.GetBeeper(_street, _avenue);
                _bag.Add(beeper);
            }
            catch (BeeperNotFoundException ex)
            {
                throw new RobotDestructionException("", ex);
            }
        }

        public void PutBeeper()
        {
            var beeper = _bag[0];
            _bag.Remove(beeper);

            beeper.Street = _street;
            beeper.Avenue = _avenue;
            _world.PlaceBeeper(beeper);
        }

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
                    return "▲";
                case Direction.South:
                    return "▼";
                case Direction.East:
                    return "▶";
                case Direction.West:
                    return "◀";
                default:
                    throw new Exception("Invalid Direction");
            }
        }

        private bool IsFacing(Direction direction)
        {
            return _direction == direction;
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

        public List<Beeper> Bag => _bag;
    }
}