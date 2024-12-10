namespace Advent_of_Code_2023
{
    public class Day06 : IDay
    {
        public long PartOne(string[] lines)
        {
            var times = lines[0].Split(":", StringSplitOptions.TrimEntries)[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse);
            var distances = lines[1].Split(":", StringSplitOptions.TrimEntries)[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse);
            var races = times.Zip(distances);
            return races.Aggregate(1L, (result, race) => result * RecordCount(race.First, race.Second));
        }

        public long PartTwo(string[] lines)
        {
            var time = long.Parse(lines[0].Split(":")[1].Replace(" ", ""));
            var distance = long.Parse(lines[1].Split(":")[1].Replace(" ", ""));
            return RecordCount(time, distance);
        }

        private static long RecordCount(long time, long distance)
        {
            var recordCount = 0L;
            for (long speed = 0; speed < time; speed++)
            {
                if ((speed * (time - speed)) > distance)
                {
                    recordCount++;
                }
            }
            return recordCount;
        }
    }
}
