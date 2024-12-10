namespace Advent_of_Code_2024
{
    public class Day04 : IDay
    {
        public long PartOne(string[] lines)
        {
            var result = 0L;

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] != 'X')
                    {
                        continue;
                    }
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (i == 0 && j == 0)
                            {
                                continue;
                            }
                            if (FindMAS(y + i, x + j, i, j, lines))
                            {
                                result++;
                            }
                        }
                    }
                }
            }

            return result;
        }

        private static bool FindMAS(int yStart, int xStart, int yOffset, int xOffset, string[] grid)
        {
            var y = yStart;
            var x = xStart;
            foreach (var c in "MAS")
            {
                if (y < 0 || x < 0 ||
                    y > grid.Length - 1 || x > grid[y].Length - 1 ||
                    grid[y][x] != c)
                {
                    return false;
                }
                y += yOffset;
                x += xOffset;
            }
            return true;
        }

        public long PartTwo(string[] lines)
        {
            var result = 0L;

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] != 'A')
                    {
                        continue;
                    }
                    var masCount = 0;
                    for (int i = -1; i <= 1; i += 2)
                    {
                        for (int j = -1; j <= 1; j += 2)
                        {
                            if (FindMAS(y - i, x - j, i, j, lines))
                            {
                                masCount++;
                            }
                        }
                    }
                    if (masCount == 2)
                    {
                        result++;
                    }
                }
            }

            return result;
        }
    }
}
