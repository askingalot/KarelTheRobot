namespace KarelTheRobot
{
    public class Wall : WorldObject
    {
        public Wall(int street, int avenue) : base(street, avenue) { }

        public override string ToString()
        {
            return "\u2591";
        }
    }
}