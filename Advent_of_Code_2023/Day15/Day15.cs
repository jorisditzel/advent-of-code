namespace Advent_of_Code_2023
{
    public class Day15 : IDay
    {
        public long PartOne(string[] lines)
        {
            var values = lines[0].Split(',');

            var result = 0;
            foreach (var value in values)
            {
                var currentValue = 0;
                foreach (var c in value)
                {
                    currentValue += c;
                    currentValue *= 17;
                    currentValue %= 256;
                }
                result += currentValue;
            }
            return result;
        }

        public long PartTwo(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
