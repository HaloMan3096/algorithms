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

public enum Suite
{
    Clubs, 
    Spades,
    Diamonds,
    Hearts,
    Joker
}

public enum CardColor 
{
    Red,
    Black
}

internal class BasicCard : ICard
{
    public BasicCard(int value, string display, Suite suit)
    {
        CardValue = value;
        DisplayValue = display;
        Suit = suit;

        switch (suit)
        {
            case Suite.Clubs:
                Color = CardColor.Black;
                break;
            
            case Suite.Spades:
                Color = CardColor.Black;
                break;
            
            case Suite.Diamonds:
                Color = CardColor.Red;
                break;
            
            case Suite.Hearts:
                Color = CardColor.Red;
                break;
            
            case Suite.Joker:
                Color = CardColor.Black;
                break;
        }
    }
    
    public int CardValue { get; set; }
    public string DisplayValue { get; set; }
    public Suite Suit { get; set; }
    public CardColor Color { get; set; }
}

internal class DeckOfCards
{
    public BasicCard[] Cards { get; set; }
    public DeckOfCards() // We default to 54 cards, 52 plus jokers
    {
        Cards = new BasicCard[54];
    }
    public DeckOfCards(int capacity) // Allow for a custom amount
    {
        Cards = new BasicCard[capacity];
    }
    public void FillBasicDeck(IEnumerable<int> values, IEnumerable<Suite> suits, IEnumerable<string> displays, bool jokers = true)
    {
        int index = 0;
        int suite = 0;

        if (jokers)
        {
            Cards[0] = new BasicCard(0, "Joker", Suite.Joker);
            Cards[1] = new BasicCard(0, "Joker", Suite.Joker);
        }
        
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
    public IEnumerable<BasicCard> OrderCards(IEnumerable<BasicCard> cards, bool ascending = false, bool descending = false, bool suite = true)
    {
        if (ascending)
        {
            cards = cards.OrderBy(card => card.CardValue);
        }
        
        if (descending)
        {
            cards = cards.OrderByDescending(card => card.CardValue);
        }

        if (suite) // Organizes the cards by suit then by number in each suit
        {
            cards = cards.OrderBy(card => card.Suit == Suite.Spades ? 1 : 2)
                         .ThenBy(card => card.Suit == Suite.Clubs ? 1 : 2)
                         .ThenBy(card => card.Suit == Suite.Diamonds ? 1 : 2)
                         .ThenBy(card => card.Suit == Suite.Hearts ? 1 : 2)
                         .ThenByDescending(card => card.CardValue);
        }
        return cards;
    }
}

internal class War
{
    private DeckOfCards? Deck { get; set; }
    private readonly int[] _values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13];
    private readonly string[] _displayValues =
        ["Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"];
    private readonly Suite[] _suits = [Suite.Spades, Suite.Clubs, Suite.Hearts, Suite.Diamonds];
    private BasicCard _player1Card, _player2Card;
    private int _player1Score, _player2Score;
    
    public void Play()
    {
        var hands = SetUp();
        var hand1 = Deck.OrderCards(hands.Item1.ToArray(), suite: true).ToArray();
        var hand2 = Deck.OrderCards(hands.Item2.ToArray(), suite: true).ToArray();

        for (int i = 0; i < hand1.Length - 1; i++)
        {
            Console.WriteLine($"Player 1 points {_player1Score} : Player 2 points {_player2Score}");
            PlayerTurn(hand1, true);
            Console.Clear();
            
            Console.WriteLine($"Player 1 points {_player1Score} : Player 2 points {_player2Score}");
            PlayerTurn(hand2, false);
            Console.Clear();
            
            Console.WriteLine($"Player 1 chose {_player1Card.DisplayValue} : Player 2 chose {_player2Card.DisplayValue}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            if (_player1Card.CardValue > _player2Card.CardValue)
            {
                _player1Score++;
                Console.WriteLine("Round goes to Player 1");
            }

            if (_player2Card.CardValue > _player1Card.CardValue)
            {
                _player2Score++;
                Console.WriteLine("Round goes to Player 2");
            }

            if (_player1Card.CardValue == _player2Card.CardValue)
            {
                Console.WriteLine("Round is a tie, no points");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        if (_player1Score > _player2Score)
        {
            Console.WriteLine($"Player 1 wins! with {_player1Score} points");
            return;
        }

        if (_player1Score < _player2Score)
        {
            Console.WriteLine($"Player 2 wins! with {_player2Score} points");
            return;
        }
        
        Console.WriteLine($"Tie with {_player1Score} points!");
    }

    private void PlayerTurn(BasicCard[] hand, bool player1 = true)
    {
        // Display cards
        DisplayHand(hand);
        // ask for pick
        Console.WriteLine("Please enter a card to play: ");
        var card = int.TryParse(Console.ReadLine(), out int cardValue);
        if (cardValue - 1 >= 0 && cardValue - 1 < hand.Length)
        {
            if (player1)
            {
                _player1Card = hand[cardValue - 1];
            }
            else
            {
                _player2Card = hand[cardValue - 1];
            }
            return;
        }
        Console.Clear();
        Console.WriteLine($"Please enter a valid card");
        Thread.Sleep(2000);
        PlayerTurn(hand, player1);
    }

    private void DisplayHand(BasicCard[] hand)
    {
        for (int i = 0; i < hand.Count(); i++)
        {
            if (hand[i].Color == CardColor.Red)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.Write($"{i + 1}: {hand[i].CardValue}, {hand[i].DisplayValue} of {hand[i].Suit}");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
    
    // -------------- -------------- -------------- -------------- --------------
    // | A          | | 2          | | 3          | | 4          | | 5          |
    // |            | |     *      | |     *      | |   *    *   | |   *     *  |
    // |     *      | |            | |     *      | |            | |      *     |
    // |            | |     *      | |     *      | |   *    *   | |   *     *  |
    // |          A | |          2 | |          3 | |          4 | |          5 |
    // -------------- -------------- -------------- -------------- --------------

    private (IEnumerable<BasicCard>, IEnumerable<BasicCard>) SetUp()
    {
        Deck = new DeckOfCards();
        Deck.FillBasicDeck(values: _values, suits: _suits, _displayValues);
        Deck.Cards = CardFunctions.ReverseFisherShuffle<BasicCard>(Deck.Cards, Deck.Cards.Length).ToArray();
        var hands = CardFunctions.Split<BasicCard>(Deck.Cards);
        return hands;
    }
}