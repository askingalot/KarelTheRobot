using System;

namespace KarelTheRobot.Library.Exceptions
{
    public class RobotDestructionException : Exception
    {
        public RobotDestructionException(string message)
            : base($"Robot Destroyed! ({message})") { }

        public RobotDestructionException(string message, Exception inner)
            : base($"Robot Destroyed! ({message})", inner) { }
    }
}