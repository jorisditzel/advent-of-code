namespace Advent_of_Code_2024
{
    public class Day02 : IDay
    {
        public long PartOne(string[] lines)
        {
            var result = 0;
            foreach (var line in lines)
            {
                var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                if (Safe(values))
                {
                    result++;
                }
            }

            return result;
        }

        private static bool Safe(int[] values)
        {
            bool safe = true;
            bool? up = null;
            for (int i = 0; i < values.Length - 1; i++)
            {
                var item = values[i];
                var next = values[i + 1];
                var diff = item - next;
                if (diff < 0 && (!(up ??= true) || diff < -3))
                {
                    safe = false;
                    break;
                }
                if (diff > 0 && ((up ??= false) || diff > 3))
                {
                    safe = false;
                    break;
                }
                if (diff == 0)
                {
                    safe = false;
                    break;
                }
            }
            return safe;
        }

        public long PartTwo(string[] lines)
        {
            var result = 0;
            foreach (var line in lines)
            {
                var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                for (int i = 0; i < values.Length; i++)
                {
                    var list = values.ToList();
                    list.RemoveAt(i);
                    if (Safe(list.ToArray()))
                    {
                        result++;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
