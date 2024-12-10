namespace Advent_of_Code_2023
{
    public class Day04 : IDay
    {
        public long PartOne(string[] lines)
        {
            return lines.Sum(l =>
            {
                var matches = Matches(l);
                return matches > 0 ? 1 << matches - 1 : 0;
            });
        }

        public long PartTwo(string[] lines)
        {
            var cards = new Dictionary<int, Card>();
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                cards.Add(i, new Card(Matches(line)));
            }

            var totalCopies = 0;
            for (int i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var matches = card.Matches;
                for (int j = 1; j <= matches && i + j < cards.Count; j++)
                {
                    cards[i + j].Copies += card.Copies;
                }
                totalCopies += card.Copies;
            }
            return totalCopies;
        }

        private static int Matches(string line)
        {
            var splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
            var numbers = line.Split(":", splitOptions)[1].Split('|', splitOptions);
            var winningNumbers = numbers[0].Split(' ', splitOptions);
            var scratchedNumbers = numbers[1].Split(' ', splitOptions);
            var overlap = scratchedNumbers.Intersect(winningNumbers);
            return overlap.Count();
        }

        private record Card(int Matches)
        {
            public int Copies { get; set; } = 1;
        }
    }
}
