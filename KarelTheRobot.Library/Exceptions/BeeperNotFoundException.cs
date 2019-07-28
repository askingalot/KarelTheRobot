namespace KarelTheRobot.Library.Exceptions
{
    public class BeeperNotFoundException : WorldException
    {
        public BeeperNotFoundException(int street, int avenue)
            : base($"No beeper found at street: {street} and avenue: {avenue}") { }
    }
}