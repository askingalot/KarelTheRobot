using System;

namespace KarelTheRobot
{
    public class Beeper : WorldObject
    {
        public Beeper(int street, int avenue) : base(street, avenue) { }

        public void SetLocation(int street, int avenue)
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