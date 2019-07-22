using System;

namespace KarelTheRobot
{
    public class Beeper
    {
        public int Street { get; set; }
        public int Avenue { get; set; }

        public Beeper() : this(1, 1) { } 
        public Beeper(int street, int avenue)
        {
            Street = street;
            Avenue = avenue;
        }

        public override string ToString()
        {
            return "\u263C";
        }
    }
}