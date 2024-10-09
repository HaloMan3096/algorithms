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