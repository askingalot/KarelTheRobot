namespace KarelTheRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var world = new World(WorldConfig.CornerBeepers);
            var karel = new Robot(world);

            karel.TurnOn();

            karel.Move();
            if (karel.IsLeftClear)
            {
                karel.TurnLeft();
            }
            else if (karel.IsRightClear)
            {
                TurnRight(karel);
            }


            karel.Move();
            karel.TurnLeft();
            karel.Move();
            if (karel.IsFrontClear)
            {
                karel.Move();
            }
            else
            {
                karel.TurnLeft();
                karel.Move();
            }
            karel.TurnOff();
        }

        private static void TurnRight(Robot karel)
        {
            karel.TurnLeft();
            karel.TurnLeft();
            karel.TurnLeft();
        }
    }
}
