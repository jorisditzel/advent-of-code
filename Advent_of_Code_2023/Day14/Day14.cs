using Advent_of_Code;

namespace Advent_of_Code_2023
{
    public class Day14 : IDay
    {
        public long PartOne(string[] lines)
        {
            var grid = lines.Select(l => l.ToArray()).ToArray();
            var transpose = grid.Transpose();

            var sum = 0;
            for (int i = 0; i < transpose.Length; i++)
            {
                var countO = 0;
                var cube = transpose.Length;
                for (int j = 0; j < transpose[i].Length; j++)
                {
                    var c = transpose[i][j];
                    if (c == '#')
                    {
                        cube = transpose[i].Length - j - 1;
                        countO = 0;
                    }
                    else if (c == 'O')
                    {
                        sum += cube - countO++;
                    }
                }
            }

            return sum;
        }

        private static char[][] Roll(char[][] grid)
        {
            var transpose = grid.Transpose();
            for (int i = 0; i < transpose.Length; i++)
            {
                var countO = 0;
                var cube = transpose.Length;
                for (int j = 0; j < transpose[i].Length; j++)
                {
                    var c = transpose[i][j];
                    if (c == '#')
                    {
                        cube = transpose[i].Length - j - 1;
                        countO = 0;
                    }
                    else if (c == 'O')
                    {
                        transpose[i][j] = '.';
                        var x = transpose[i].Length - cube + countO++;
                        transpose[i][x] = 'O';
                    }
                }
            }
            return transpose.Transpose();
        }

        private static int Weight(char[][] grid)
        {
            var sum = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                sum += (grid.Length - i) * grid[i].Count(x => x == 'O');
            }
            return sum;
        }

        public long PartTwo(string[] lines)
        {
            var grid = lines.Select(l => l.ToArray()).ToArray();
            grid.Print();
            var rotations = 1000000000L;
            List<int> weights = [];
            Dictionary<string, long> hashes = new() { { grid.Hash(), 0 } };
            long start = 0;
            long cylce = 0;
            for (long i = 1; i <= rotations; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    grid = Roll(grid);
                    grid = grid.RotateClockwise();
                }
                Console.WriteLine();
                grid.Print();

                weights.Add(Weight(grid));

                var hash = grid.Hash();
                if (hashes.TryGetValue(hash, out start))
                {
                    cylce = i - start;
                    break;
                }
                hashes.Add(hash, i);
            }

            var index = (int)(start + ((rotations - start) % cylce));
            return weights[index - 1];
        }
    }
}
