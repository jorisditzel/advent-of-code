namespace Advent_of_Code_2023
{
    public class Day02 : IDay
    {
        public long PartOne(string[] lines) => GetResult(lines).part1;

        public long PartTwo(string[] lines) => GetResult(lines).part2;

        private static (long part1, long part2) GetResult(string[] lines)
        {
            var bag = new Bag(12, 13, 14);
            var count1 = 0;
            var count2 = 0;
            foreach (var line in lines)
            {
                var gameReveal = line.Split(':');
                var game = int.Parse(gameReveal[0].Split(' ')[1]);
                var revealed = new Bag(gameReveal[1].Split(";"));
                count1 += revealed <= bag ? game : 0;
                count2 += revealed;
            }

            return (count1, count2);
        }
    }
}
