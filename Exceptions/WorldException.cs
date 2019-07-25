using System;

namespace KarelTheRobot.Exceptions
{
    public class WorldException : Exception
    {
        public WorldException(string message) : base(message) { }
    }
}