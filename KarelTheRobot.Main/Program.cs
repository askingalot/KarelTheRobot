using KarelTheRobot.Library;
using KarelTheRobot.Library.Config;

namespace KarelTheRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            //ClearBorderBeepers();
            MoveBeeperAcrossBoundary();
        }

        private static void MoveBeeperAcrossBoundary()
        {
            var world = new World(WorldConfig.FromJson("crossTheBoundary.json"));
            var karel = new Robot(world);

            var streetCount = 0;

            karel.TurnOn();

            karel.Move();
            karel.PickBeeper();

            while (karel.IsFrontClear)
            {
                karel.Move();
            }

            karel.TurnLeft();

            while (!karel.IsRightClear)
            {
                streetCount++;
                karel.Move();
            }

            TurnRight(karel);
            karel.Move();

            while (!karel.IsRightClear)
            {
                karel.Move();
            }

            TurnRight(karel);

            for (int i = 0; i < streetCount; i++)
            {
                karel.Move();
            }

            karel.TurnLeft();

            while (karel.IsFrontClear)
            {
                karel.Move();
            }

            TurnAround(karel);
            karel.Move();
            karel.PutBeeper();
            TurnAround(karel);
            karel.Move();

            karel.TurnOff();
        }

        private static void ClearBorderBeepers()
        {
            var world = new World(WorldConfig.FromJson("borderBeepers.json"));
            var karel = new Robot(world);

            int beeperCount = 0;

            karel.TurnOn();

            while (karel.IsFrontClear)
            {
                karel.Move();
            }

            karel.TurnLeft();

            while (karel.IsNextToBeeper)
            {
                world.Log($"I've got {beeperCount} beepers!");
                beeperCount++;
                karel.PickBeeper();
                if (!karel.IsFrontClear)
                {
                    karel.TurnLeft();
                }
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

        private static void TurnAround(Robot karel)
        {
            karel.TurnLeft();
            karel.TurnLeft();
        }
    }
}
