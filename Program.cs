namespace KarelTheRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var world = new World();
            var karel = new Robot(world);
            karel.Move();
            if (karel.LeftIsClear) {
                karel.TurnLeft();
            } else if (karel.RightIsClear) {
                TurnRight(karel);
            }
            karel.Move();
            karel.TurnLeft();
            karel.Move();
            if (karel.FrontIsClear)
            {
                karel.Move();
            }
            else
            {
                karel.TurnLeft();
                karel.Move();
            }
        }

        private static void TurnRight(Robot karel)
        {
            karel.TurnLeft();
            karel.TurnLeft();
            karel.TurnLeft();
        }
    }
}
