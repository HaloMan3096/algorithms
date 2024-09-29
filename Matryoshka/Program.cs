namespace ConsoleApp1;
using System.Linq;

internal abstract class Program
{
    // Preps the senario 
    private static void Main(string[] args)
    {
        var nestingDolls = createNestingDolls(10);
        
        countListOfDolls(nestingDolls);
        Console.ReadKey();
        Console.Clear();
        
        orderFromSmallestToLargest(nestingDolls);
        Console.ReadKey();
        Console.Clear();
        
        findTheSignedOne(nestingDolls);
        Console.ReadKey();
        Console.Clear();
    }

    private static List<NestingDoll> createNestingDolls(int numOfDolls)
    {
        // creates 10 dolls with random number of dolls in them
        var rng = new Random();
        // We only want one doll to be signed
        bool singed;
        var nestingDolls = new List<NestingDoll>();
        for (var i = 0; i < numOfDolls; i++)
        {
            switch (i)
            {
                case 7:
                    singed = true;
                    break;
                default:
                    singed = false;
                    break;
            }
            nestingDolls.Add(new NestingDoll(rng.Next(1, 20), singed));
        }
        // Just making sure the list is shuffled up before returning it :)
        IEnumerable<NestingDoll> randomNestingDolls = nestingDolls.OrderBy(x => rng.Next());
        return randomNestingDolls.ToList();
    }

    private static void countListOfDolls(List<NestingDoll> nestingDolls)
    {
        // counts the total amount
        var count = 0;
        foreach (var doll in nestingDolls)
        {
            var currentDollCount = doll.CountInnerDolls();
            Console.WriteLine(currentDollCount);
            count += currentDollCount;
        }
        Console.WriteLine($"Count: {count}");
    }

    private static void orderFromSmallestToLargest(List<NestingDoll> nestingDolls)
    {
        // List the dolls from lest to most, number of inner dolls
        IEnumerable<NestingDoll> smallestDolls = nestingDolls.OrderBy(dolls => dolls.CountInnerDolls());
        foreach (var doll in smallestDolls)
        {
            Console.WriteLine($"count: {doll.CountInnerDolls()}");
        }
    }

    public static void findTheSignedOne(List<NestingDoll> nestingDolls)
    {
        foreach (var doll in nestingDolls)
        {
            Console.WriteLine($"count: {doll.CountInnerDolls()}");
            if (doll.checkIfSigned())
            {
                Console.WriteLine($"Found the signed doll: {doll}, is the doll singed?: {doll.checkIfSigned()}, Count: {doll.CountInnerDolls()}");
                break;
            }
        }
    }
}

internal class NestingDoll
{
    // Pretty simple doll class that holds a private reference to another doll
    private readonly NestingDoll _innerDoll;
    private NestingDoll GetInnerDoll() { return _innerDoll; }
    private bool _isSigned = false;
    
    public NestingDoll(int howManyInnerDolls = 1, bool signed = false) // When creating a doll the user can tell how many dolls they want nested
    {
        // If the user leaves it blank we assume there is just the one
        // Until we get to 0 we recursively call this. we use the ? operator rather than an if statement here
        _isSigned = signed;
        _innerDoll = howManyInnerDolls <= 0 ? null : new NestingDoll(howManyInnerDolls - 1); 
        // Once there are none left we set the inner doll to null (probably not the best way)
    }

    /// <summary>
    /// Counts the number of dolls that are nested in this instance
    /// </summary>
    /// <returns>The number of dolls nested in this instance</returns>
    public int CountInnerDolls()
    {
        var count = 0; // We store a var to hold how many dolls
        var currentDoll = this._innerDoll; // we set the current doll to the inner doll
        while (currentDoll != null) // As long as the current doll isn't null we keep looping
        {
            ++count; // We increment the count
            currentDoll = currentDoll.GetInnerDoll(); // We set the current doll to the inner doll
        }
        return count; // Return the count
    }

    public void Sign()
    {
        _isSigned = true;
    }

    public bool checkIfSigned()
    {
        return _isSigned;
    }
}