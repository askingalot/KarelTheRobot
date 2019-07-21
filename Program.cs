namespace KarelTheRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var world = new World();
            var karel = new Robot(world);
            karel.Move();
            karel.TurnLeft();
            karel.Move();
            karel.TurnLeft();
            karel.Move();
            karel.Move();
        }
    }
}
