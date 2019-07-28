# Karel the Robot

[Karel](https://en.wikipedia.org/wiki/Karel_(programming_language)) is just a robot trying to make it in the world.

This is a C# implementation of the classic programming teaching tool. It runs on .NET Core, and is cross platform (Windows, Mac OS X and Linux). It's intended to be a fun way to practice with the basics of the C# programming language.

## Getting Started
1. Clone this repository.
1. Choose a challenge from the available challenges (`*.json` files) or make your own. (see the documentation below)
1. Write code in the [KarelTheRobot.Main/Program.cs](./KarelTheRobot.Main/Program.cs) file to solve the challenge.
1. Run the program.
    - **In Linux or Mac OS X:**
        1. Open the Terminal
        1. Navigate to the repo's root directory.
        1. Run the program using the `run` command
            ```sh
            $ ./run
            ```
    * **In Windows**
        1. Open the Command Prompt (cmd.exe)
        1. Navigate to the repo's root directory.
        1. Run the program using the `run.bat` command
            ```sh
            > run
            ```
## The World
The robot's world is a rectangle surrounded by walls. It may contain internal walls and beepers for the robot to interact with. You may think it's a lonely place, but, fortunately, most robots enjoy time to themselves.

The world is made up of streets and avenues. Streets run east-west and avenues run north-south.

A world looks something like this:
```
   ┏┯┯┯┯┯┯┯┯┯┯┯┯┯┯┯┯┯┯┯┯┓
   ┣····················┫
   ┣····················┫
   ┣····················┫
   ┣····················┫
   ┣····················┫
   ┣····················┫
   ┣····················┫
   ┣····················┫
   ┗┷┷┷┷┷┷┷┷┷┷┷┷┷┷┷┷┷┷┷┷┛
```


## Beepers
A small plastic cone that emits a quiet beeping noise that the robot can sense. Any number of beepers may be on any street corner. The robot carries a bag that can hold any number of beepers.

A beeper looks something like: `☼`

## Robot
The robot is controlled by the code you write. It can move around the world, pick up beepers to put in its bag, and take beepers from its bag to place in the world.

A robot looks something like a triangle pointed in the robot's current direction of movement.

### Available Robot Commands

A robot has the following built in commands:
1. `TurnOn` - Power up the robot. The robot MUST be turned on before receiving any commends otherwise it will be destroyed. If the robot is already tunred on, turning it on again will destroy it.
1. `TurnOff` - Power off the robot. The robot MUST be turned off before the application exists or it will be destroyed. The robot MUST be on when it is turned off, or it will be restored.
1. `Move` - Move the robot one space in it's current direction. If the robot hits a wall it will be destroyed.
1. `TurnLeft` - Turn the robot 90 degrees to the left. The robot will remain in the same location.
1. `PickBeeper` - Pick up a beeper at the robot's current location and put it in the robot's bag. If there is no beeper at the robot's current location the robot will be destroyed.
1. `PutBeeper` - Take a beeper from the robot's bag and place it in the world at the robot's current location. If there is no beeper in the robot's bag, the robot will be destroyed.

### Sensing the World
The robot can sense the world around it using these properties.
1. `IsFrontClear` - Is the location in front of the robot free to move into?
1. `IsLeftClear` - Is the location to the left of the robot free to move into?
1. `IsRightClear` - Is the location to the right of the robot free to move into?
1. `IsNextToBeeper` - Is there a beeper at the robot's current location?
1. `AreAnyBeepersInBag` - Are there any beepers in the robot's bag?
1. `IsFacingNorth` - Is the robot facing north? (i.e. toward the top of the screen)
1. `IsFacingSouth` - Is the robot facing south? (i.e. toward the bottom of the screen)
1. `IsFacingEast` - Is the robot facing east? (i.e. toward the right of the screen)
1. `IsFacingWest` - Is the robot facing west? (i.e. toward the left of the screen)

## Configuring a World
Configuration is done by way of a JSON file containing an object with the following properties:

1. `challengeText` - The description of the challenge to be completed in this world.
1. `streetCount` - The number of streets in the world.
1. `avenueCount` - The number of avenues in the worl.d
1. `robot` - An object containing two keys, `street` and `avenue`, that specifies the initial location for the robot.
1. `beepers` - (Optional) An array describing the initial locations of each beeper in the world.
1. `walls` - (Optional) An array describing the locations of each _internal_ wall in the world. Note: you do not need to list the outer boundaries of the world.
 
### An Example
```json
{
  "challengeText": "Pick up the beeper in front of you and move it to the opposite location across the world",
  "streetCount": 20,
  "avenueCount": 20,
  "robot" : { "street": 10, "avenue": 1 },
  "beepers": [
    {"street": 10, "avenue": 2}
  ],
  "walls": [
    {"street": 2, "avenue": 10},
    {"street": 18, "avenue": 10}
  ]
}
```



## Learn More
* https://en.wikipedia.org/wiki/Karel_(programming_language)
* https://cs.mtsu.edu/~untch/karel/index.html
