namespace Advent_of_Code_2023
{
    public class Day01 : IDay
    {
        private readonly Dictionary<string, byte> letterNumberCombinations = new()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };                

        public long PartOne(string[] lines)
        {
            return GetResult(lines, false);
        }

        public long PartTwo(string[] lines)
        {
            return GetResult(lines, true);
        }

        private long GetResult(string[] lines, bool includeLetters)
        {
            int sum = 0;
            var all = GetAllNumbers(includeLetters);
            foreach (var line in lines)
            {
                var containing = all.Where(line.Contains);
                var first = containing.MinBy(line.IndexOf);
                var last = containing.MaxBy(line.LastIndexOf);
                var digit = (ParseNumber(first) * 10) + ParseNumber(last);
                sum += digit;
            }
            return sum;
            //return Task.CompletedTask;
        }

        private byte ParseNumber(string number)
        {
            if (letterNumberCombinations.TryGetValue(number, out var value))
            {
                return value;
            }
            return byte.Parse(number);
        }

        private IEnumerable<string> GetAllNumbers(bool includeLetters)
        {
            foreach (var combination in letterNumberCombinations)
            {
                if (includeLetters)
                {
                    yield return combination.Key;
                }
                yield return combination.Value.ToString();
            }
        }
    }
}
