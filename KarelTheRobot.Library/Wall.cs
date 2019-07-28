namespace KarelTheRobot.Library
{
    internal class Wall : WorldObject
    {
        internal Wall(int street, int avenue) : base(street, avenue) { }

        public override string ToString()
        {
            return "\u2591";
        }
    }
}