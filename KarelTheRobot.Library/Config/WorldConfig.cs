using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace KarelTheRobot.Library.Config
{
    public class WorldConfig
    {
        internal List<Beeper> Beepers { get; }
        internal List<Wall> Walls { get; }

        internal int StreetCount { get; }
        internal int AvenueCount { get; }

        internal int RobotStreet { get; }
        internal int RobotAvenue { get; }

        internal string ChallengeText { get; }


        internal WorldConfig(
            int streetCount = 10,
            int avenueCount = 20,
            int robotStreet = 1,
            int robotAvenue = 1,
            List<Beeper> beepers = null, 
            List<Wall> walls = null,
            string challengeText = "")
        {
            StreetCount = streetCount;
            AvenueCount = avenueCount;
            RobotStreet = robotStreet;
            RobotAvenue = robotAvenue;
            Beepers = beepers ?? new List<Beeper>();
            Walls = walls ?? new List<Wall>();
            ChallengeText = challengeText;
        }

        public static WorldConfig FromJson(string configFileName) {
            var text = File.ReadAllText(configFileName);
            var jsonConfig = 
                JsonConvert.DeserializeObject<JsonConfigModel>(text);
            return new WorldConfig(
                jsonConfig.streetCount,
                jsonConfig.avenueCount,
                jsonConfig.robot.street,
                jsonConfig.robot.avenue,
                jsonConfig.beepers?.Select(b => new Beeper(b.street, b.avenue)).ToList(),
                jsonConfig.walls?.Select(w => new Wall(w.street, w.avenue)).ToList(),
                jsonConfig.challengeText
            );
        }

        public static readonly WorldConfig Empty = new WorldConfig();
        public static readonly WorldConfig CornerBeepers =
            new WorldConfig(
                challengeText: "Pick up beepers in the corers of the world",
                streetCount: 10,
                avenueCount: 20,
                beepers: new List<Beeper> {
                    new Beeper(1, 1),
                    new Beeper(1, 20),
                    new Beeper(10, 1),
                    new Beeper(10, 20),
                }
            );
        public static readonly WorldConfig Steps = 
            new WorldConfig(
                challengeText: "Navigate the steps while picking up all beepers in the world",
                streetCount: 20,
                avenueCount: 40,
                robotStreet: 1,
                robotAvenue: 7,
                beepers: new List<Beeper> {
                    new Beeper(1, 10),
                    new Beeper(2, 11),
                    new Beeper(3, 12),
                    new Beeper(4, 13),
                },
                walls: new List<Wall> {
                    new Wall(1, 11), new Wall(1, 12), new Wall(1, 13),
                    new Wall(2, 12), new Wall(2, 13),
                    new Wall(3, 13),
                }
            );
    }
}