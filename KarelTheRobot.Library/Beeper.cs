namespace KarelTheRobot.Library
{
    internal class Beeper : WorldObject
    {
        internal Beeper(int street, int avenue) : base(street, avenue) { }

        internal void SetLocation(int street, int avenue)
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