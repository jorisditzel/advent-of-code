namespace Advent_of_Code_2023
{
    public class Day09 : IDay
    {
        public long PartOne(string[] lines)
        {
            var count = 0;
            foreach (var line in lines)
            {
                int[] nextSequence = line.Split(' ').Select(int.Parse).ToArray();
                count += nextSequence[^1];
                while (nextSequence.Any(i => i != 0))
                {
                    nextSequence = GetNextSequence(nextSequence).ToArray();
                    count += nextSequence[^1];
                }
            }

            return count;
        }

        public long PartTwo(string[] lines)
        {
            var count = 0;
            foreach (var line in lines)
            {
                int[] nextSequence = line.Split(' ').Select(int.Parse).ToArray();
                List<int> firsts = [nextSequence[0]];
                while (nextSequence.Any(i => i != 0))
                {
                    nextSequence = GetNextSequence(nextSequence).ToArray();
                    firsts.Add(nextSequence[0]);
                }
                firsts.Reverse();
                var result = firsts.Aggregate(0, (x, y) => y - x);
                count += result;
            }

            return count;
        }

        //private static IEnumerable<IEnumerable<int>> GetSequences(int[] sequence)
        //{
        //    do
        //    {
        //        sequence = GetNextSequence(sequence).ToArray();
        //        yield return sequence;
        //    }
        //    while (sequence.Any(i => i != 0));
        //}

        private static IEnumerable<int> GetNextSequence(int[] sequence)
        {
            for (int i = 0; i < sequence.Length - 1; i++)
            {
                yield return sequence[i + 1] - sequence[i];
            }
        }
    }
}
