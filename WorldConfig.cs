using System.Collections.Generic;

namespace KarelTheRobot
{
    public class WorldConfig
    {
        public List<Beeper> Beepers { get; }
        public List<Wall> Walls { get; }

        public WorldConfig(List<Beeper> beepers = null, List<Wall> walls = null)
        {
            Beepers = beepers ?? new List<Beeper>();
            Walls = walls ?? new List<Wall>();
        }

        public static readonly WorldConfig Empty = new WorldConfig();
        public static readonly WorldConfig CornerBeepers =
            new WorldConfig(new List<Beeper> {
                new Beeper(1, 1),
                new Beeper(1, 20),
                new Beeper(10, 1),
                new Beeper(10, 20),
            });
        public static readonly WorldConfig Steps = 
            new WorldConfig(
                new List<Beeper> {
                    new Beeper(1, 10),
                    new Beeper(2, 11),
                    new Beeper(3, 12),
                    new Beeper(4, 13),
                },
                new List<Wall> {
                    new Wall(1, 11), new Wall(1, 12), new Wall(1, 13),
                    new Wall(2, 12), new Wall(2, 13),
                    new Wall(3, 13),
                }
            );
    }
}