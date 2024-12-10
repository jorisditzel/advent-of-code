namespace Advent_of_Code_2023
{
    public enum HandType
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }

    public record Hand(char[] Cards, int Bid, bool JokerRules) : IComparable<Hand>
    {
        private List<char> CardOrder => JokerRules
            ? ['A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J']
            : ['A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2'];

        private HandType Type
        {
            get
            {
                var cardCount = Cards.GroupBy(h => h).ToDictionary(h => h.Key, h => h.Count());
                if (JokerRules && cardCount.TryGetValue('J', out var count))
                {
                    var highestCount = cardCount.Where(c => c.Key != 'J').OrderByDescending(c => c.Value).ThenBy(c => CardOrder.IndexOf(c.Key));
                    if (highestCount.Any())
                    {
                        cardCount[highestCount.First().Key] += count;
                        cardCount.Remove('J');
                    }
                }
                var maxCount = cardCount.Max(g => g.Value);
                return cardCount.Count switch
                {
                    1 => HandType.FiveOfAKind,
                    2 when maxCount == 4 => HandType.FourOfAKind,
                    2 => HandType.FullHouse,
                    3 when maxCount == 3 => HandType.ThreeOfAKind,
                    3 => HandType.TwoPair,
                    4 => HandType.OnePair,
                    _ => HandType.HighCard
                };
            }
        }

        public int CompareTo(Hand other)
        {
            if (Type != other.Type)
            {
                return Type.CompareTo(other.Type);
            }
            for (int i = 0; i < Cards.Length; i++)
            {
                if (Cards[i] == other.Cards[i])
                {
                    continue;
                }
                var cardIndex = CardOrder.IndexOf(Cards[i]);
                var otherIndex = CardOrder.IndexOf(other.Cards[i]);
                return cardIndex > otherIndex ? -1 : 1;
            }
            return 0;
        }
    }
}
