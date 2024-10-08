using System.Diagnostics;

namespace Connect_four;

class Program
{
    static void Main(string[] args)
    {
        Board board = new Board(5, 5);
        loop(board);
    }

    static void loop(Board board)
    {
        // Draw
        Draw(board);
        
        // Dropping
        Console.WriteLine("Enter a colum number: ");
        var input = int.TryParse(Console.ReadLine(), out int col) ? col - 1 : -1;
        if (input > board.GridBoard[0].Length - 1)
        {
            Console.WriteLine("You entered an invalid number");
            loop(board);
        }
        else
        {
            // Drop
            board.Drop(input);
            
            // Check for match
            Console.WriteLine(board.IsMatch());
            if (board.IsMatch())
            {
                Console.WriteLine("You entered a match");
                return;
            }
            
            // Swap players
            Player.Instance.ChangePlayer();
        }

        if (board.IsMatch())
        {
            Console.WriteLine("Its a TIE");
            return;
        }
        
        // Loop
        loop(board);
    }

    static void Draw(Board board)
    {
        DrawTopBottom();
        DrawMid(board);
        DrawTopBottom();
    }

    static void DrawTopBottom()
    {
        Console.WriteLine("+---------------+");
    }

    static void DrawMid(Board board)
    {
        foreach (var spaces in board.GridBoard)
        {
            Console.Write("|");
            foreach (var space in spaces)
            {
                if (space.state == State.Empty)
                {
                    Console.Write("[ ]");
                }

                if (space.state == State.Black)
                {
                    Console.Write("[X]");
                }

                if (space.state == State.White)
                {
                    Console.Write("[O]");
                }
            }
            Console.WriteLine("|");
        }
    }
}

public enum State { Empty, Black, White };
class Board
{
    public readonly Space[][] GridBoard;

    public Board(int width, int height)
    {
        GridBoard = new Space[width][];
        for (int i = 0; i < height; i++)
        {
            GridBoard[i] = new Space[width];
        }
        FillBoard();
    }

    public bool IsFull()
    {
        foreach (var spaces in GridBoard)
        {
            for (int j = 0; j < spaces.Length; j++)
            {
                if (spaces[j].state == State.Empty)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void FillBoard()
    {
        foreach (var t in GridBoard)
        {
            for (var j = 0; j < t.Length; j++)
            {
                t[j] = new Space
                {
                    state = State.Empty
                };
            }
        }
    }

    private (int, int) GetSpace(Space space)
    {
        var q = (-1, -1);
        for (int i = 0; i < GridBoard.Length; i++)
        {
            for (int j = 0; j < GridBoard[i].Length; j++)
            {
                if (GridBoard[i][j] == space)
                {
                    q = (i, j);
                }
            }
        }
        return q;
    }

    public bool IsMatch()
    {
        var h = Horizontal();
        if (h == true)
        {
            return true;
        }
        var v = Vertical();
        if (v == true)
        {
            return true;
        }
        
        return false;
    }

    private bool Horizontal()
    {
        foreach (var spaces in GridBoard)
        {
            List<State> temp = [spaces[0].state];
            for (var i = 1; i < spaces.Length; i++)
            {
                if (temp[0] != spaces[i].state)
                {
                    temp.Clear();
                    temp.Add(spaces[i].state);
                    continue;
                }

                if (spaces[i].state != State.Empty)
                {
                    temp.Add(spaces[i].state);
                }
            }

            if (temp.Count >= 4)
            {
                return true;
            }
            else
            {
                temp.Clear();
            }
        }

        return false;
    }

    private bool Vertical()
    {
        for (int i = 0; i < GridBoard[0].Length - 1; i++)
        {
            List<State> temp = [GridBoard[i][0].state];
            for (int j = 0; j < GridBoard.Length - 1; j++)
            {
                if (GridBoard[i][j].state != temp[0])
                {
                    temp.Clear();
                    temp.Add(GridBoard[i][j].state);
                    continue;
                }

                if (GridBoard[i][j].state != State.Empty)
                {
                    temp.Add(GridBoard[i][j].state);
                }
            }

            if (temp.Count >= 4)
            {
                return true;
            }
            else
            {
                temp.Clear();
            }
        }
        return false;
    }

    public void Drop(int col)
    {
        Space space = GetLowestSpace(col);
        var q = GetSpace(space).Item1;
        Console.WriteLine(q);
        space.SetState(Player.Instance.InstanceState);
    }

    private Space GetLowestSpace(int col)
    {
        Space space = null;
        for (int i = 0; i < GridBoard.Length; i++)
        {
            if (GridBoard[i][col].state == State.Black || GridBoard[i][col].state == State.White)
            {
                break;
            }
            
            if (GridBoard[i][col].state == State.Empty)
            {
                space = GridBoard[i][col];
            }
        }
        return space;
    }
}

class Space
{
    public State state = State.Empty;

    public void SetState(State state)
    {
        this.state = state;
    }
}

class Player
{
    private static Player? _instance;
    private State _state = State.White ;
    private Player()
    {
    }

    public static Player? Instance
    {
        get { return _instance ??= new Player(); }
    }

    public State InstanceState => _state;

    public void ChangePlayer()
    {
        _state = Instance._state switch
        {
            State.Black => State.White,
            State.White => State.Black,
            _ => _state
        };
    }
}