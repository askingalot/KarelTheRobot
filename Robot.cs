using System;
using System.Collections.Generic;
using System.Linq;
using KarelTheRobot.Exceptions;

namespace KarelTheRobot
{
    public class Robot : WorldObject
    {
        private bool _isOn = false;

        private Direction _direction = Direction.East;
        private readonly World _world;

        private readonly List<Beeper> _bag = new List<Beeper>();
        public List<Beeper> Bag => _bag;

        public Robot(World world) : base(1, 1)
        {
            AddListenerToCheckOnOffStatusAtAppShutdown();
            _world = world;
            _world.PlaceRobot(this);
            _world.Display();
        }

        public bool IsFrontClear =>
            ConfirmOnThen(() =>
                _world.ObjectTypeAt(PositionAt(_direction)) != ObjectType.Wall);
        public bool IsLeftClear =>
            ConfirmOnThen(() =>
                _world.ObjectTypeAt(PositionAt(LeftDirection)) != ObjectType.Wall);
        public bool IsRightClear =>
            ConfirmOnThen(() =>
                _world.ObjectTypeAt(PositionAt(RightDirection)) != ObjectType.Wall);

        public bool IsNextToBeeper =>
            ConfirmOnThen(() =>
                _world.ObjectTypeAt(Street, Avenue) == ObjectType.Beeper);

        public bool AreAnyBeepersInBag => ConfirmOnThen(() => _bag.Any());

        public bool IsFacingNorth => ConfirmOnThen(() => IsFacing(Direction.North));
        public bool IsFacingSouth => ConfirmOnThen(() => IsFacing(Direction.South));
        public bool IsFacingEast => ConfirmOnThen(() => IsFacing(Direction.East));
        public bool IsFacingWest => ConfirmOnThen(() => IsFacing(Direction.West));

        public void TurnOn()
        {
            if (_isOn)
            {
                throw new RobotDestructionException("Robot is already on");
            }
            _isOn = true;
        }
        public void TurnOff()
        {
            ConfirmOnThen(() => _isOn = false);
        }

        public void PickBeeper()
        {
            ConfirmOnThen(() =>
            {
                try
                {
                    var beeper = _world.GetBeeper(Street, Avenue);
                    _bag.Add(beeper);
                }
                catch (BeeperNotFoundException ex)
                {
                    throw new RobotDestructionException("", ex);
                }
            });
        }

        public void PutBeeper()
        {
            ConfirmOnThen(() =>
            {
                var beeper = _bag[0];
                _bag.Remove(beeper);

                beeper.SetLocation(Street, Avenue);
                _world.PlaceBeeper(beeper);
            });
        }

        public void Move()
        {
            ConfirmOnThen(() =>
            {
                (Street, Avenue) = PositionAt(_direction);
                _world.Display();

                if (_world.ObjectTypeAt(Street, Avenue) == ObjectType.Wall)
                {
                    throw new RobotWallCollisionException();
                }
            });
        }

        public void TurnLeft()
        {
            ConfirmOnThen(() =>
            {
                _direction = LeftDirection;
                _world.Display();
            });
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
                    return ">";
                case Direction.West:
                    return "<";
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
                    return (Street + 1, Avenue);
                case Direction.South:
                    return (Street - 1, Avenue);
                case Direction.East:
                    return (Street, Avenue + 1);
                case Direction.West:
                    return (Street, Avenue - 1);
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

        private T ConfirmOnThen<T>(Func<T> ifOnAction)
        {
            if (!_isOn)
            {
                throw new RobotDestructionException("Cannot use the robot when it is off.");
            }
            return ifOnAction();
        }

        private void ConfirmOnThen(Action ifOnAction)
        {
            if (!_isOn)
            {
                throw new RobotDestructionException("Cannot use the robot when it is off.");
            }
            ifOnAction();
        }

        private void AddListenerToCheckOnOffStatusAtAppShutdown()
        {
            AppDomain.CurrentDomain.ProcessExit += (sender, args) =>
            {
                if (_isOn)
                {
                    throw new RobotDestructionException(
                        "Please turn the robot off before exiting the program.");
                }
            };
        }
    }
}