using Advent_of_Code;

namespace Advent_of_Code_2023
{
    public class Day11 : IDay
    {
        private const int EXPENSION_FACTOR = 1000000;

        public long PartOne(string[] lines)
        {
            var map = ExpandVertical(ExpandVertical(lines.Select(s => s.ToCharArray()).ToArray()).ToArray().Transpose()).ToArray().Transpose();
            foreach (var line in map)
            {
                Console.WriteLine(line);
            }

            var galaxies = Galaxies(map).ToArray();
            return CountDistances(galaxies);
        }

        private static IEnumerable<char[]> ExpandVertical(char[][] map)
        {
            foreach (var line in map)
            {
                yield return line;
                if (line.All(c => c == '.'))
                {
                    yield return line;
                }
            }
        }

        private static IEnumerable<(int x, int y)> Galaxies(char[][] map)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == '#')
                    {
                        yield return (x, y);
                    }
                }
            }
        }

        public long PartTwo(string[] lines)
        {
            List<(int x, int y)> expandedGalaxies = [];
            int expandedY = 0;
            for (int y = 0; y < lines.Length; y++)
            {
                expandedY += lines[y].All(c => c == '.') ? EXPENSION_FACTOR : 1;
                int expandedX = 0;
                for (int x = 0; x < lines[y].Length; x++)
                {
                    var expand = true;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i][x] != '.')
                        {
                            expand = false;
                        }
                    }
                    expandedX += expand ? EXPENSION_FACTOR : 1;
                    if (lines[y][x] == '#')
                    {
                        expandedGalaxies.Add((expandedX, expandedY));
                    }
                }
            }
            var galaxies = expandedGalaxies.ToArray();
            return CountDistances(galaxies);
        }

        private static long CountDistances((int x, int y)[] galaxies)
        {
            var count = 0L;
            for (int i = 0; i < galaxies.Length; i++)
            {
                for (int j = i + 1; j < galaxies.Length; j++)
                {
                    var x = Math.Abs(galaxies[j].x - galaxies[i].x);
                    var y = Math.Abs(galaxies[j].y - galaxies[i].y);
                    count += x + y;
                }
            }
            return count;
        }
    }
}
