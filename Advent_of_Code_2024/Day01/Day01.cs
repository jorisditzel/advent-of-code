namespace Advent_of_Code_2024
{
    public class Day01 : IDay
    {
        public long PartOne(string[] lines)
        {
            var leftList = new List<int>();
            var rightList = new List<int>();
            foreach (var line in lines)
            {
                var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                leftList.Add(int.Parse(values[0]));
                rightList.Add(int.Parse(values[1]));
            }

            leftList.Sort();
            rightList.Sort();
            var result = 0;
            for (int i = 0; i < leftList.Count; i++)
            {
                result += Math.Abs(leftList[i] - rightList[i]);
            }

            return result;
        }

        public long PartTwo(string[] lines)
        {
            var leftList = new List<int>();
            var rightList = new List<int>();
            foreach (var line in lines)
            {
                var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                leftList.Add(int.Parse(values[0]));
                rightList.Add(int.Parse(values[1]));
            }

            var result = 0;
            foreach(var item in leftList)
            {
                result += item * rightList.Count(i => i == item);
            }
            return result;
        }
    }
}
