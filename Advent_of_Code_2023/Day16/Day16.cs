using Advent_of_Code;

namespace Advent_of_Code_2023
{
    public class Day16 : IDay
    {
        private HashSet<(int x, int y, Direction direction)> GetPath(int x, int y, Direction direction, char[][] grid)
        {
            HashSet<(int x, int y, Direction direction)> path = [];
            while (y >= 0 && y < grid.Length && x >= 0 && x < grid[0].Length)
            {
                Console.WriteLine((x, y, direction.ToString()));
                if (!path.Add((x, y, direction)))
                {
                    break;
                }
                var c = grid[y][x];
                if (c == '.' ||
                    (c == '-' && (direction == Direction.East || direction == Direction.West)) ||
                    (c == '|' && (direction == Direction.North || direction == Direction.South)))
                {
                    x = direction switch
                    {
                        Direction.East => x + 1,
                        Direction.West => x - 1,
                        _ => x
                    };
                    y = direction switch
                    {
                        Direction.North => y - 1,
                        Direction.South => y + 1,
                        _ => y
                    };
                }
                else if (c == '\\')
                {
                    switch (direction)
                    {
                        case Direction.North:
                            x--;
                            direction = Direction.West;
                            break;
                        case Direction.South:
                            x++;
                            direction = Direction.East;
                            break;
                        case Direction.East:
                            y++;
                            direction = Direction.South;
                            break;
                        case Direction.West:
                            y--;
                            direction = Direction.North;
                            break;
                    }
                }
                else if (c == '/')
                {
                    switch (direction)
                    {
                        case Direction.North:
                            x++;
                            direction = Direction.East;
                            break;
                        case Direction.South:
                            x--;
                            direction = Direction.West;
                            break;
                        case Direction.East:
                            y--;
                            direction = Direction.North;
                            break;
                        case Direction.West:
                            y++;
                            direction = Direction.South;
                            break;
                    }
                }
                else if (c == '-')
                {
                    var leftPath = GetPath(x - 1, y, Direction.West, grid);
                    var rightPath = GetPath(x + 1, y, Direction.East, grid);
                    path.UnionWith(leftPath);
                    path.UnionWith(rightPath);
                    break;
                }
                else if (c == '|')
                {
                    var upPath = GetPath(x, y - 1, Direction.North, grid);
                    var downPath = GetPath(x, y + 1, Direction.South, grid);
                    path.UnionWith(upPath);
                    path.UnionWith(downPath);
                    break;
                }
            }
            return path;
        }


        public long PartOne(string[] lines)
        {
            var grid = lines.ToGrid();
            var energised = GetPath(0, 0, Direction.East, grid);
            return energised.Select(e => (e.x, e.y)).Distinct().Count();
        }

        public long PartTwo(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}
