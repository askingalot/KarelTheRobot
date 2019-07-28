using System;

namespace KarelTheRobot.Library.Exceptions
{
    public class WorldException : Exception
    {
        public WorldException(string message) : base(message) { }
    }
}