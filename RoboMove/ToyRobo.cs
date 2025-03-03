
public class Robo
{
    private int _x;
    private int _y;
    private Direction _facing;
    private bool _placed;
    private const int GRID_SIZE = 5;

    public enum Direction
    {
        NORTH, SOUTH, WEST, EAST
    }

    public Robo()
    {
        this._x = -1;
        this._y = -1;
        this._facing = Direction.NORTH;
        this._placed = false;
    }

    public void Place(int x, int y, Direction facing)
    {
        if (x >= 0 && x < GRID_SIZE && y >= 0 && y < GRID_SIZE)
        {
            this._x = x;
            this._y = y;
            this._facing = facing;
            this._placed = true;
        }

    }

    public void Move()
    {
        if (!_placed)
            return;

        switch (_facing)
        {
            case Direction.NORTH:
                if (_y < GRID_SIZE - 1)
                    _y++;
                break;
            case Direction.EAST:
                if (_x < GRID_SIZE - 1)
                    _x++;
                break;
            case Direction.WEST:
                if (_x > 0)
                    _x--;
                break;
            case Direction.SOUTH:
                if (_y > 0)
                    _y--;
                break;
        }
    }
    public void Left()
    {
        if (!_placed)
            return;
        _facing = TurnLeft(_facing);
    }
    public void Right()
    {
        if (!_placed)
            return;
        _facing = TurnRight(_facing);
    }

    public void Report()
    {
        if (_placed)
            Console.WriteLine($"{_x},{_y},{_facing}");
    }

    private static Direction TurnLeft(Direction facing)
    {
        return facing switch
        {
            Direction.NORTH => Direction.WEST,
            Direction.EAST => Direction.NORTH,
            Direction.SOUTH => Direction.EAST,
            Direction.WEST => Direction.SOUTH,
            _ => throw new InvalidOperationException("Invalid Direction"),
        };
    }
    private static Direction TurnRight(Direction facing)
    {
        return facing switch
        {
            Direction.NORTH => Direction.EAST,
            Direction.EAST => Direction.SOUTH,
            Direction.SOUTH => Direction.WEST,
            Direction.WEST => Direction.NORTH,
            _ => throw new InvalidOperationException("Invalid Direction"),
        };
    }

    public static void Main(string[] args)
    {
        Robo robo = new Robo();
        String input;
        while (true)
        {
            input = Console.ReadLine();
            string[] checkInput = input.Split(" ");
            string inputUpperCase = checkInput[0].ToUpper();
            if (inputUpperCase == "PLACE" && checkInput.Length == 2)
            {
                string[] position = checkInput[1].Split(",");
                int x = int.Parse(position[0]);
                int y = int.Parse(position[1]);
                Direction facing = (Direction)Enum.Parse(typeof(Direction), position[2].ToUpper());
                robo.Place(x, y, facing);
            }
            else if (inputUpperCase == "MOVE")
            {
                robo.Move();
            }
            else if (inputUpperCase == "LEFT")
            {
                robo.Left();
            }
            else if (inputUpperCase == "RIGHT")
            {
                robo.Right();
            }
            else if (inputUpperCase == "REPORT")
            {
                robo.Report();
                return;
            }
            else
            {
                Console.WriteLine("Enter Valid Input");
            }

        }
    }
}
