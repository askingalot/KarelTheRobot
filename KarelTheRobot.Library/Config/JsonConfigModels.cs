using System.Collections.Generic;

namespace KarelTheRobot.Library.Config
{
    internal class Location
    {
        public int street { get; set; }
        public int avenue { get; set; }
    }

    internal class JsonConfigModel
    {
        public string challengeText { get; set; }
        public int streetCount { get; set; }
        public int avenueCount { get; set; }
        public Location robot { get; set; }
        public List<Location> beepers { get; set; }
        public List<Location> walls { get; set; }
    }
}