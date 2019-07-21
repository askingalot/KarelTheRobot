using System;

namespace KarelTheRobot.Exceptions
{
    public class RobotDestructionException : Exception
    {
        public RobotDestructionException(string message)
            : base($"Robot Destroyed! ({message})") { }
    }
}