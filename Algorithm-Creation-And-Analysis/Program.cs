namespace Algorithm_Creation_And_Analysis;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        var doll = new NestingDoll();
        var count = doll.CountInnerDolls();
        Console.WriteLine($"Count: {count}");
    }
}

internal class NestingDoll
{
    // Pretty simple doll class that holds a private reference to another doll
    private readonly NestingDoll _innerDoll;
    private NestingDoll GetInnerDoll() { return _innerDoll; }
    
    public NestingDoll(int howManyInnerDolls = 1) // When creating a doll the user can tell how many dolls they want nested
    {
        // If the user leaves it blank we assume there is just the one
        // Until we get to 0 we recursively call this. we use the ? operator rather than an if statement here
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
}