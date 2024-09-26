using System.Runtime.CompilerServices;
namespace Week_2_HW;

class Program
{
    MainLoop mainLoop = new();
    const int WIDTH_VALUE = 5;
    const int HEIGHT_VALUE = 6;
    static void Main(string[] args)
    {
        Grid area = new(WIDTH_VALUE,HEIGHT_VALUE);
        MainLoop.Run(area);
    }
}

class MainLoop
{
    public static void Run(Grid area) 
    {
        // Draw
        DrawArea(area);

        // Get Input
        // we can escape by typing 'q' below
        int col = (GetIntInput("Please enter a Column: ") - 1);
        int row = (GetIntInput("Please enter a Row: ") - 1);
            
        // Use the input
        Point selected = area.get_value(col, row);
        Console.WriteLine(String.Format("{0},{1},{2}", col, row, selected)); // Debug TODO: remove this
        if (selected.value == "-") // The selected values were not on the grid
        {
            Run(area); // We just re-run the function
        }
        Console.ReadLine();
        // We now know the selected value is real so now we can destroy it
        area.destroy_value(col, row); // we "destroy" the selected point
        area.update_value(col, row);
            
        // repeat
        Console.Clear();
        Run(area); // we re-run the function here 
    }

    static int GetIntInput(String q) // The same as column but for row
    {
        Console.Write(q);
        string s = Console.ReadLine();
        int i = 0; // Get and save the input
        if (int.TryParse(s, out int number)) // Try to parse the string into an int
        {
            i = number; // if it works then we set it and leave
        }
        else
        {
            if (s.ToLower() == "q") // else we check if the input was our escape key
            {
                exit();
            }
            Console.WriteLine("Please input a Valid input"); // if it isn't our escape key we ask again
            GetIntInput(q); // to ask again we call the function again
        }
        return i;
    }

    /// <summary>
    /// Exits the game with no errors
    /// </summary>
    static void exit()
    {
        System.Environment.Exit(0);
    }

    /// <summary>
    /// Takes in a grid and writes it to the console dynamiclly
    /// </summary>
    /// <param name="area"></param>
    private static void DrawArea(Grid area)
    {
        Draw_bounds(area);
        Display_grid(area);
        Draw_bounds(area);
    }
    
    private static void Draw_bounds(Grid area)
    {
        Console.Write("-+");
        for (int i = 0; i < area.grid[0].Length; i++)
        {
            Console.Write("-");
        }
        Console.Write("+");
        Console.WriteLine();
    }

    private static void Display_grid(Grid area)
    {
        for (int x = 0; x < area.grid.Length; x++) {
            Console.Write((x + 1) + "|");
            for (int y = 0; y < area.grid[x].Length; y++) {
                Console.Write(area.grid[x][y].value);
            }
            Console.Write("|");
            Console.WriteLine();
        }
    }
}

class Grid {
    public Point[][] grid;

    /// <summary>
    /// Takes in a width and a height and sets up the grid with "random" values
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public Grid( int width, int height ) {
        grid = new Point[height][]; // Set up the outside array
        for (int i = 0; i < grid.Length; i++) // Set up the inner arrays
        {
            grid[i] = new Point[width];
        }
        this.fill_grid();
    }

    private void fill_grid()
    {
        // we need to loop through the grid and set values
        for (int x = 0; x < this.grid.Length; x++) // Loop through the outer arrays
        {
            for (int y = 0; y < this.grid[x].Length; y++) // Loop through the inner arrays
            {
                this.grid[x][y] = new Point(Point.get_random_value()); // Set the "random" value
            }
        }
    }

    public Point get_value(int x, int y) 
    {
        if (x > this.grid.Length || y > this.grid[0].Length) // check the requested value is in the grid
        {
            return new Point(); // if it is not in we return an empty point
        }
        return this.grid[x][y]; // otherwise the point is valid and returned to the caller
    }

    public Grid destroy_value(int x, int y)
    {
        this.grid[x][y].value = "-"; // we set the old point to a value of "-"
        return this; // return this grid
    }

    public void update_value(int x, int y)
    {
        if (x >= 1) // we check the y value of the point and make sure its not the top one
        {
            this.grid[x][y].value = this.grid[x - 1][y].value; // we get the point one space above's value and set the new one
        }
        if (x == 0) // if at the top we need a new random value
        {
            this.grid[x][y].value = Point.get_random_value();
            return; // we also want to escape the recursion when were at the top
        }
        this.update_value(x - 1, y); // as long as were not at the top of the list we want to keep moving points down
    }

    /// <summary>
    /// Takes in a point and returns a list of points if the there is a match
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public List<Point> check_match(int x, int y)
    {
        List<Point> result = new();

        // Vertical
        switch (y)
        {
            case 0:
                // we are at the top of the grid
                
                break;
        }
        
        return result;
    }
}

class Point // A simple class that holds a value of our choice and has some basic functions
{
    // points with a value of - are dead cells and will be replaced
    public string value;
    public Point(string value = "-")
    {
        this.value = value; // if the caller does not set a value for the point it is left as -
    }

    public static String get_random_value() // We need to be able to set a "random" value 
    {
        Random random = new();
        return random.Next(10).ToString();
    }
}

// Making a candy crush ala program
// Have a grid of numbers shown and prompt the user to enter a colum and a row
// . 1 2 3 4 5
// 1|- - - - -
// 2|- - - - -
// ...

// create a /point/ class that has a value a row and a colum
// value char '1'
// row int 0
// colum int 1 
// this point is a '1' at (0, 1)

// The program holds an array of Points