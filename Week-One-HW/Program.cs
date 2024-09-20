namespace Week_One_HW;

class Program
{
    static void Main(string[] args)
    {
        List<string> data = ["Hello, World!", "Wow Second!", "Wow Third!"];
        Console.WriteLine(data);
        Console.WriteLine("\n\rConstant: ");
        Constant(data);
        Console.WriteLine("\n\rLinear: ");
        Linear(data);
        Console.WriteLine("\n\rQuadratic: ");
        Quadratic(data);
    }

    static void Constant(List<String> args)
    {
        Console.WriteLine(args[0]);
    }

    static void Linear(List<String> args)
    {
        foreach (var s in args)
        {
            Console.WriteLine(s);
        }
    }

    static void Quadratic(List<String> args)
    {
        foreach (var s in args)
        {
            foreach (var v in args)
            {
                Console.WriteLine(s + " " + v);
            }
        }
    }
}