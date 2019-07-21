namespace KarelTheRobot.Exceptions
{
    public class RobotWallCollisionException : RobotDestructionException
    {
        public RobotWallCollisionException() : base("Hit a wall") { }
    }
}