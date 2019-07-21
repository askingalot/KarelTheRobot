using System;

namespace KarelTheRobot
{
    public class Beeper
    {
        public int Street { get; set; }
        public int Avenue { get; set; }

        public override string ToString()
        {
            return "\u263C";
        }
    }
}