# Toy Robot Simulator

The Toy Robot Simulator is a C# application that simulates a toy robot moving on a 5x5 square tabletop.

## Description

- The application is a simulation of a toy robot moving on a square tabletop,
  of dimensions 5 units x 5 units.
- There are no other obstructions on the table surface.
- The robot is free to roam around the surface of the table, but must be
  prevented from falling to destruction. Any movement that would result in the
  robot falling from the table must be prevented, however further valid movement
  commands must still be allowed.
 
The application that can read in commands of the following form

    PLACE X,Y,FACING
    MOVE
    LEFT
    RIGHT
    REPORT

- PLACE will put the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST.
- The origin (0,0) can be considered to be the SOUTH WEST most corner.
- The first valid command to the robot is a PLACE command, after that, any sequence of commands may be issued,
  in any order, including another PLACE command. The application should discard all commands in the
  sequence until a valid PLACE command has been executed.
- MOVE will move the toy robot one unit forward in the direction it is currently
  facing.
- LEFT and RIGHT will rotate the robot 90 degrees in the specified direction
  without changing the position of the robot.
- REPORT will announce the X,Y and F (Facing direction) of the robot.
- Any move that would cause the robot to fall must be ignored.

## Example Input and Output:
    
a)

	PLACE 0,0,NORTH
    MOVE
    REPORT

	Output: 0,1,NORTH

b)

	PLACE 0,0,NORTH
	LEFT
	REPORT
	
	Output: 0,0,WEST

c)

	PLACE 1,2,EAST
	MOVE
	MOVE
	LEFT
	MOVE
	REPORT

	Output: 3,3,NORTH

## Implementation Details:

The solution consists of the following components:

- `Robo` class: Main class implementing the robot's behavior
- `Direction` enum: Represents the four possible directions
- `TurnLeft` and `TurnRight` methods: Handle rotation logic
- `Main` method: Processes user input and controls the simulation

## Testing:

The application has been tested with various scenarios to ensure it functions correctly:

- Basic movement and rotation
- Edge detection to prevent falling
- Command validation and error handling
- Complex movement sequences
- Multiple placements
