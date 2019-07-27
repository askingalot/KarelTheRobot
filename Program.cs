namespace KarelTheRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var world = new World(WorldConfig.Steps);
            var karel = new Robot(world);

            var beeperCount = 0;

            karel.TurnOn();

            while (beeperCount < 4)
            {
                world.Log(beeperCount.ToString());
                while (!karel.IsFacingEast)
                {
                    karel.TurnLeft();
                }

                while (karel.IsFrontClear && !karel.IsNextToBeeper)
                {
                    karel.Move();
                }

                if (karel.IsNextToBeeper)
                {
                    karel.PickBeeper();
                    beeperCount++;
                }

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
