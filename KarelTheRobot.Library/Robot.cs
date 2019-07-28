using System;
using System.Collections.Generic;
using System.Linq;
using KarelTheRobot.Library.Exceptions;

namespace KarelTheRobot.Library
{
    /// <Summary>
    ///  A robot, perhaps named Karel, is a happy servant to you, the intrepid developer.
    ///  Command it to navigate its world, avoid obsticals, and pick up and put down beepers.
    /// </Summary>
    public class Robot : WorldObject
    {
        private bool _isOn = false;

        private Direction _direction = Direction.East;
        private readonly World _world;

        private readonly List<Beeper> _bag = new List<Beeper>();

        /// <Summary>
        ///  Make a new Robot and put it in world
        /// </Summary>
        public Robot(World world) : base(1, 1)
        {
            AddListenerToCheckOnOffStatusAtAppShutdown();
            _world = world;
            _world.PlaceRobot(this);
            _world.Display();
        }

        /// <Summary>
        ///  Is the location in front of the robot free to move into?
        /// </Summary>
        public bool IsFrontClear =>
            ConfirmOnThen(() =>
                _world.ObjectTypeAt(PositionAt(_direction)) != ObjectType.Wall);
        /// <Summary>
        ///  Is the location to the left of the robot free to move into?
        /// </Summary>
        public bool IsLeftClear =>
            ConfirmOnThen(() =>
                _world.ObjectTypeAt(PositionAt(LeftDirection)) != ObjectType.Wall);
        /// <Summary>
        ///  Is the location to the right of the robot free to move into?
        /// </Summary>
        public bool IsRightClear =>
            ConfirmOnThen(() =>
                _world.ObjectTypeAt(PositionAt(RightDirection)) != ObjectType.Wall);

        /// <Summary>
        ///  Is there a beeper at the robot's current location?
        /// </Summary>
        public bool IsNextToBeeper =>
            ConfirmOnThen(() =>
                _world.ObjectTypeAt(Street, Avenue) == ObjectType.Beeper);

        /// <Summary>
        ///  Are there any beepers in the robot's bag?
        /// </Summary>
        public bool AreAnyBeepersInBag => ConfirmOnThen(() => _bag.Any());

        /// <Summary>
        ///  Is the robot facing north? (i.e. toward the top of the screen)
        /// </Summary>
        public bool IsFacingNorth => ConfirmOnThen(() => IsFacing(Direction.North));
        /// <Summary>
        ///  Is the robot facing south? (i.e. toward the bottom of the screen)
        /// </Summary>
        public bool IsFacingSouth => ConfirmOnThen(() => IsFacing(Direction.South));
        /// <Summary>
        ///  Is the robot facing east? (i.e. toward the right of the screen)
        /// </Summary>
        public bool IsFacingEast => ConfirmOnThen(() => IsFacing(Direction.East));
        /// <Summary>
        ///  Is the robot facing west? (i.e. toward the left of the screen)
        /// </Summary>
        public bool IsFacingWest => ConfirmOnThen(() => IsFacing(Direction.West));

        /// <Summary>
        ///  Power up the robot. 
        ///  The robot MUST be turned on before receiving any commends otherwise it will be destroyed.
        ///  If the robot is already tunred on, turning it on again will destroy it.
        /// </Summary>
        public void TurnOn()
        {
            if (_isOn)
            {
                throw new RobotDestructionException("Robot is already on");
            }
            _isOn = true;
        }

        /// <Summary>
        ///  Power off the robot. 
        ///  The robot MUST be turned off before the application exists or it will be destroyed.
        ///  The robot MUST be on when it is turned off, or it will be destoyed.
        /// </Summary>
        public void TurnOff()
        {
            ConfirmOnThen(() => _isOn = false);
        }

        /// <Summary>
        ///  Pick up a beeper at the robot's current location and put it in the robot's bag.
        ///  If there is no beeper at the robot's current location the robot will be destroyed.
        /// </Summary>
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

        /// <Summary>
        ///  Take a beeper from the robot's bag and place it in the world at the robot's current location.
        ///  If there is no beeper in the robot's bag, the robot will be destroyed.
        /// </Summary>
        public void PutBeeper()
        {
            ConfirmOnThen(() =>
            {
                if (!_bag.Any())
                {
                    throw new RobotDestructionException("Bag is empty");
                }
                var beeper = _bag[0];
                _bag.Remove(beeper);

                beeper.SetLocation(Street, Avenue);
                _world.PlaceBeeper(beeper);
            });
        }

        /// <Summary>
        ///  Move the robot one space in it's current direction.
        ///  If the robot hits a wall it will be destroyed.
        /// </Summary>
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


        /// <Summary>
        ///  Turn the robot to the left. The robot will remain in the same location.
        /// </Summary>
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

        internal void SetLocation(int street, int avenue)
        {
            Street = street;
            Avenue = avenue;
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