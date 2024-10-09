using System.Collections;

namespace CardGames;
using AlgorithmsLibrary;

class Program
{
    static void Main(string[] args)
    {
        War w = new War();
        w.Play();
    }
}

internal class BasicCard : ICard
{
    public BasicCard(int value, string display, string suit)
    {
        CardValue = value;
        DisplayValue = display;
        Suit = suit;
    }
    
    public int CardValue { get; set; }
    public string DisplayValue { get; set; }
    public string Suit { get; set; }
}

internal class DeckOfCards
{
    public BasicCard[] Cards { get; set; }

    // We default to 52 cards
    public DeckOfCards()
    {
        Cards = new BasicCard[54];
    }

    // Allow for a custom amount
    public DeckOfCards(int capacity)
    {
        Cards = new BasicCard[capacity];
    }

    public void FillBasicDeck(IEnumerable<int> values, IEnumerable<string> suits, IEnumerable<string> displays)
    {
        int index = 0;
        int suite = 0;

        Cards[0] = new BasicCard(10, "Joker", "Joker");
        Cards[1] = new BasicCard(10, "Joker", "Joker");
        for (int i = 2; i <= Cards.Length - 1; i++)
        {
            var value = values.ElementAt(index);
            Cards[i] = new BasicCard(value, displays.ElementAt(index), suits.ElementAt(suite));
            index++;
            
            if (index > values.Count() - 1)
            {
                index = 0;
                suite++;
            }
        }
    }
}

internal class War
{
    private DeckOfCards? Deck { get; set; }
    private readonly int[] _values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13];
    private readonly string[] _displayValues =
        ["Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"];
    private readonly string[] _suits = ["Clubs", "Spades", "Diamonds", "Hearts"];
    
    public void Play()
    {
        SetUp();
    }

    private void SetUp()
    {
        Deck = new DeckOfCards();
        Deck.FillBasicDeck(values: _values, suits: _suits, _displayValues);
        Deck.Cards = CardFunctions.Shuffle<BasicCard>(Deck.Cards, Deck.Cards.Length).ToArray();
        var hands = CardFunctions.Split<BasicCard>(Deck.Cards);
    }
}