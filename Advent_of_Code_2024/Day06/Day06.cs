using Advent_of_Code;

namespace Advent_of_Code_2024
{
    public class Day06 : IDay
    {
        public long PartOne(string[] lines)
        {
            var grid = lines.ToGrid();
            HashSet<(int x, int y)> path = [];
            TryGetCoordinate(grid, ['^', '>', 'v', '<'], out var x, out var y, out var orientation);
            while (true)
            {
                path.Add((x, y));
                if (y > 0 && y < grid.Length - 1 && x > 0 && x < grid[y].Length - 1)
                {
                    var nextX = orientation switch
                    {
                        '^' => x,
                        '>' => x + 1,
                        'v' => x,
                        '<' => x - 1,
                        _ => x
                    };
                    var nextY = orientation switch
                    {
                        '^' => y - 1,
                        '>' => y,
                        'v' => y + 1,
                        '<' => y,
                        _ => y
                    };
                    var next = grid[nextY][nextX];
                    if (next != '#')
                    {
                        grid[y][x] = 'X';
                        grid[nextY][nextX] = orientation;
                        x = nextX;
                        y = nextY;
                    }
                    else
                    {
                        orientation = orientation switch
                        {
                            '^' => '>',
                            '>' => 'v',
                            'v' => '<',
                            '<' => '^',
                            _ => throw new ArgumentOutOfRangeException(nameof(orientation))
                        };
                        grid[y][x] = orientation;
                    }
                }
                else
                {
                    grid[y][x] = 'X';
                    break;
                }
            }
            grid.Print();
            return path.Count;
        }

        private static bool TryGetCoordinate(char[][] lines, char[] c, out int x, out int y, out char orientation)
        {
            for (y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                for (x = 0; x < line.Length; x++)
                {
                    orientation = line[x];
                    if (c.Contains(orientation))
                    {
                        return true;
                    }
                }
            }
            x = 0;
            orientation = 'X';
            return false;
        }

        public long PartTwo(string[] lines)
        {
            var obstacleCount = 0;
            var coordinates = new List<(int x, int y)>();
            for (int y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    coordinates.Add((x, y));
                }
            }
            Parallel.ForEach(coordinates, coordinate =>
            {
                var grid = lines.ToGrid();
                grid[coordinate.y][coordinate.x] = 'O';
                if (HasLoop(grid))
                {
                    Interlocked.Increment(ref obstacleCount);
                }
            });
            return obstacleCount;
        }

        private static bool HasLoop(char[][] grid)
        {
            HashSet<(int x, int y, char orientation)> path = [];
            if (!TryGetCoordinate(grid, ['^', '>', 'v', '<'], out var x, out var y, out var orientation))
            {
                return false;
            }
            while (true)
            {
                if (!path.Add((x, y, orientation)))
                {
                    return true;
                }
                if (y > 0 && y < grid.Length - 1 && x > 0 && x < grid[y].Length - 1)
                {
                    var nextX = orientation switch
                    {
                        '^' => x,
                        '>' => x + 1,
                        'v' => x,
                        '<' => x - 1,
                        _ => x
                    };
                    var nextY = orientation switch
                    {
                        '^' => y - 1,
                        '>' => y,
                        'v' => y + 1,
                        '<' => y,
                        _ => y
                    };
                    var next = grid[nextY][nextX];
                    if (next != '#' && next != 'O')
                    {
                        grid[y][x] = 'X';
                        grid[nextY][nextX] = orientation;
                        x = nextX;
                        y = nextY;
                    }
                    else
                    {
                        orientation = orientation switch
                        {
                            '^' => '>',
                            '>' => 'v',
                            'v' => '<',
                            '<' => '^',
                            _ => throw new ArgumentOutOfRangeException(nameof(orientation))
                        };
                        grid[y][x] = orientation;
                    }
                }
                else
                {
                    grid[y][x] = 'X';
                    break;
                }
            }
            return false;
        }
    }
}
