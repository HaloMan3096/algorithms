using System.Collections;

namespace AlgorithmsLibrary;
using System.Linq;
using System.Collections.Generic;

public static class CardFunctions
{
    public static IEnumerable<T> Shuffle<T>(IEnumerable<T> cards, int size)
    {
        var rng = new Random();
        var shuffledList =
            cards.
                Select(x => new { Number = rng.Next(), Item = x}).
                OrderBy(x => rng.Next()).
                Select(x => x.Item).
                Take(size);
        return shuffledList;
    }

    public static IEnumerable<T> FisherShuffle<T>(IEnumerable<T> cards, int size)
    {
        var rng = new Random();
        for (int i = size; i < 0; i--)
        {
            int j = rng.Next(0, i);
            cards = Swap(cards.ToList(), i, j);
        }
        return cards;
    }

    public static IEnumerable<T> ReverseFisherShuffle<T>(IEnumerable<T> cards, int size)
    {
        var rng = new Random();
        for (int i = 0; i < size - 2; i++)
        {
            var j = rng.Next(i, size);
            cards = Swap(cards.ToList(), i, j);
        }
        return cards;
    }

    private static IEnumerable<T> Swap<T>(List<T> list, int i, int j)
    {
        var tmp1 = list.ElementAt(i);
        var tmp2 = list.ElementAt(j);
        list[i] = tmp2;
        list[j] = tmp1;
        
        return list;
    }

    public static (IEnumerable<T>, IEnumerable<T>) Split<T>(IEnumerable<T> cards, bool shuffle = false)
    {
        if (shuffle)
        {
            var shuffledList = Shuffle(cards, cards.Count());
        }

        var index = cards.Count();
        var item1 = cards.Take(index / 2);
        var item2 = cards.Skip(index / 2);
        return (item1, item2);
    }
}