using System.Diagnostics;

// TODO: Check for matches after the initial pass through?
// TODO: Possible bug when you have to matches that intersect, does it remove a value its not supposed to?
// TODO: Maybe convert Point class to a struct?

namespace Week_2_HW;

internal static class Program
{
    private const int WidthValue = 5;
    private const int HeightValue = 6;

    private static void Main()
    {
        Grid area = new(WidthValue,HeightValue);
        MainLoop.Run(area);
    }
}

internal static class MainLoop
{
    public static void Run(Grid area) 
    {
        // Draw
        DrawArea(area);

        // Get Input
        // we can escape by typing 'q' below
        var selectedRow = (GetIntInput("Please enter a Row: ") - 1);
        var selectedCol = (GetIntInput("Please enter a Column: ") - 1);
            
        // Use the input
        var selected = area.get_value(selectedRow, selectedCol);
        Console.WriteLine($"{selectedRow},{selectedCol},{selected.value}"); // TODO: remove this
        if (selected.value == null) // The selected values were not on the grid
        {
            Run(area); // We just re-run the function
        }
        Console.ReadLine();
        // We now know the selected value is real so now we can destroy it
        area.destroy_value(selectedRow, selectedCol); // we "destroy" the selected point
        area.update_value(selectedRow, selectedCol);
        
        // Draw again
        DrawArea(area);
        
        // We now check for matches 
        var matches = area.check_match();
        if (matches.Count == 0)
        {
            Console.WriteLine("No match");
        }

        if (matches.Count >= 1)
        {
            Console.WriteLine($"Matched {matches.Count} matches");
            foreach (var match in matches)
            {
                area.score++;
                foreach (var point in match)
                {   // should add safety if the point doesn't exist aka (-1,-1)
                    var q = area.get_coordinate_from_point(point);
                    area.update_value(q.Item2, q.Item1);
                }
            }
        }
        Console.ReadLine();
        
        // repeat
        Console.Clear();
        Run(area); // we re-run the function here 
    }

    private static int GetIntInput(string q) // The same as column but for row
    {
        Console.Write(q);
        var s = Console.ReadLine();
        var i = 0; // Get and save the input
        if (int.TryParse(s, out var number)) // Try to parse the string into an int
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
    /// Takes in a grid and writes it to the console dynamically
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
        for (var i = 0; i < area.grid[0].Length; i++)
        {
            Console.Write("-");
        }
        Console.Write("+");
        Console.WriteLine();
    }

    private static void Display_grid(Grid area)
    {
        for (var x = 0; x < area.grid.Length; x++) {
            Console.Write((x + 1) + "|");
            for (var y = 0; y < area.grid[x].Length; y++) {
                Console.Write(area.grid[x][y].value);
            }
            Console.Write("|");
            Console.WriteLine();
        }
    }
}

internal class Grid {
    public Point[][] grid;
    public int score = 0;

    /// <summary>
    /// Takes in a width and a height and sets up the grid with "random" values
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public Grid( int width, int height ) {
        grid = new Point[height][]; // Set up the outside array
        for (var i = 0; i < grid.Length; i++) // Set up the inner arrays
        {
            grid[i] = new Point[width];
        }
        this.fill_grid();
    }

    private void fill_grid()
    {
        // we need to loop through the grid and set values
        foreach (var t in this.grid)
        {
            for (var y = 0; y < t.Length; y++) // Loop through the inner arrays
            {
                t[y] = new Point(Point.get_random_value()); // Set the "random" value
            }
        }
    }

    public Point get_value(int x, int y) 
    {
        if (x > this.grid.Length || y > this.grid[0].Length) // check the requested value is in the grid
        {
            return null;  // if it is not in we return an empty point
        }
        return this.grid[x][y]; // otherwise the point is valid and returned to the caller
    }

    public (int, int) get_coordinate_from_point(Point p)
    {
        var result = (-1, -1);
        for (var y = 0; y < this.grid.Length; y++)
        {
            for (var x = 0; x < this.grid[y].Length; x++)
            {
                if (this.grid[y][x] == p)
                {
                    result = (x, y);
                }
            }
        }
        return result;
    }

    public void destroy_value(int x, int y)
    {
        this.grid[x][y].value = "-"; // we set the old point to a value of "-"
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
        this.update_value(x - 1, y); // as long as were not at the top of the list, we want to keep moving points down
    }
    
    // |--------------x----------------|=
    // [ [Point, Point, Point, Point...]|
    // , [Point, Point, Point, Point...]y
    // , [Point, Point, Point, Point...]|
    // ...]                             =
    
    public List<List<Point>> check_match()
    {
        List<List<Point>> matches = [];

        // HORIZONTAL
        foreach (var row in this.grid)
        {   // [Point, Point, Point...]
            List<Point> match = [row[0]];
            for (var x = 1; x < row.Length; x++)
            {
                if (row[x].value != match[0].value && match.Count < 3)
                {
                    match.Clear();
                    match.Add(row[x]);
                    continue;
                }
                if (row[x].value == match[0].value)
                {
                    match.Add(row[x]);
                }
            }
            if (match.Count >= 3)
            {
                matches.Add(match);
            }
            else
            {
                match.Clear();
            }
        }
        
        // VERTICAL
        foreach (var p in this.grid.Last())
        {
            List<Point> match = [p]; // Starting point
            var y = this.get_coordinate_from_point(p).Item1;
            for (var x = (this.grid.Length - 1); x > 0; x--)
            {
                if (this.grid[x][y].value != match[0].value && match.Count < 3)
                {
                    match.Clear();
                    match.Add(this.grid[x][y]);
                    continue;
                }

                if (this.grid[x][y].value == match[0].value)
                {
                    match.Add(this.grid[x][y]);
                }
            }
            if (match.Count >= 3)
            {
                matches.Add(match);
            }
            else
            {
                match.Clear();
            }
        }
        
        return matches;
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
        return random.Next(1,5).ToString();
    }
}