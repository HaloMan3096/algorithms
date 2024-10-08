namespace CardGames;
using AlgorithmsLibrary;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}

internal class BasicCard : ICard
{
    public int CardValue { get; set; }
    public int DisplayValue { get; set; }
}

internal class DeckOfCards
{
    public ICard[] Cards { get; set; }

    public DeckOfCards()
    {
        Cards = new ICard[52];
    }
}

internal class War
{
    
}