namespace Advent_of_Code_2023
{
    public class Day07 : IDay
    {
        public long PartOne(string[] lines) => GetResult(lines, false);

        public long PartTwo(string[] lines) => GetResult(lines, true);

        private static long GetResult(string[] lines, bool jokerRules)
        {
            var hands = lines.Select(line =>
            {
                var split = line.Split(' ');
                return new Hand(split[0].ToCharArray(), int.Parse(split[1]), jokerRules);
            }).Order();

            var index = 0;
            return hands.Aggregate(0, (result, hand) => result + (index++ + 1) * hand.Bid);
        }
    }
}
