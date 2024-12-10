using System.Text.RegularExpressions;

namespace Advent_of_Code_2024
{
    public class Day03 : IDay
    {
        public long PartOne(string[] lines)
        {
            var regex = new Regex(@"mul\(\d{1,3},\d{1,3}\)");
            var result = 0L;
            foreach (var line in lines)
            {
                var matches = regex.Matches(line);
                result += matches.Aggregate(0, (a, b) => a + Multiply(b.Value));
            }
            return result;
        }

        private static int Multiply(string operation)
        {
            var numbers = operation.Replace("mul(", "").Replace(")", "");
            return numbers.Split(",").Aggregate(1, (a, b) => a * int.Parse(b));
        }

        public long PartTwo(string[] lines)
        {
            var regex = new Regex(@"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)");
            var result = 0L;
            var enabled = true;
            foreach (var line in lines)
            {
                var matches = regex.Matches(line);
                foreach (var match in matches.ToList())
                {
                    if (match.Value == "do()")
                    {
                        enabled = true;
                    }
                    else if (match.Value.Equals("don't()"))
                    {
                        enabled = false;
                    }
                    else if (enabled)
                    {
                        result += Multiply(match.Value);
                    }
                }
            }
            return result;
        }
    }
}
