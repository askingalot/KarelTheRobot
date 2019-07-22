using System.Collections.Generic;

namespace KarelTheRobot
{
    public class WorldConfig
    {
        public List<Beeper> Beepers { get; }

        public WorldConfig(List<Beeper> beepers = null)
        {
            Beepers = beepers ?? new List<Beeper>();
        }

        public static readonly WorldConfig Empty = new WorldConfig();
        public static readonly WorldConfig CornerBeepers = 
            new WorldConfig(new List<Beeper> {
                new Beeper(1, 1),
                new Beeper(1, 20),
                new Beeper(10, 1),
                new Beeper(10, 20),
            });
    }
}