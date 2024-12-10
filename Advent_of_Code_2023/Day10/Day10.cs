namespace Advent_of_Code_2023
{
    public class Day10 : IDay
    {
        public long PartOne(string[] lines)
        {
            var points = Points(lines, out _);
            return points.Count / 2;
        }

        private static char GetPrintSymbol(char symbol, char startSymbol)
        {
            return symbol switch
            {
                '|' => '║',
                '-' => '═',
                'L' => '╚',
                'J' => '╝',
                '7' => '╗',
                'F' => '╔',
                'S' => GetPrintSymbol(startSymbol, startSymbol),
                _ => throw new ArgumentOutOfRangeException(),
            }; ;
        }

        private char[][] GetLargeSymbol(char c)
        {
            return c switch
            {
                '|' =>
                [
                    ['X', '║', 'X'],
                    ['X', '║', 'X'],
                    ['X', '║', 'X']
                ],
                '-' =>
                [
                    ['X', 'X', 'X'],
                    ['═', '═', '═'],
                    ['X', 'X', 'X']
                ],
                'L' =>
                [
                    ['X', '║', 'X'],
                    ['X', '╚', '═'],
                    ['X', 'X', 'X']
                ],
                'J' =>
                [
                    ['X', '║', 'X'],
                    ['═', '╝', 'X'],
                    ['X', 'X', 'X']
                ],
                '7' =>
                [
                    ['X', 'X', 'X'],
                    ['═', '╗', 'X'],
                    ['X', '║', 'X']
                ],
                'F' =>
                [
                    ['X', 'X', 'X'],
                    ['X', '╔', '═'],
                    ['X', '║', 'X']
                ],
                'X' =>
                [
                    ['X', 'X', 'X'],
                    ['X', 'X', 'X'],
                    ['X', 'X', 'X']
                ],
                _ => throw new ArgumentOutOfRangeException(nameof(c)),
            };
        }

        public long PartTwo(string[] lines)
        {
            var mainPipes = Points(lines, out var startSymbol);

            var result = new char[lines.Length * 3][];
            for (int y = 0; y < lines.Length; y++)
            {
                result[y * 3] = new char[lines[y].Length * 3];
                result[y * 3 + 1] = new char[lines[y].Length * 3];
                result[y * 3 + 2] = new char[lines[y].Length * 3];
                for (int x = 0; x < lines[y].Length; x++)
                {
                    var symbol = mainPipes.Contains((x, y)) ? lines[y][x] : 'X';
                    var c = GetLargeSymbol(symbol == 'S' ? startSymbol : symbol);
                    for (int i = 0; i < c.Length; i++)
                    {
                        for (int j = 0; j < c[i].Length; j++)
                        {
                            result[y * 3 + j][x * 3 + i] = c[j][i];
                        }
                    }
                }
            }

            bool ready = false;
            while (!ready)
            {
                ready = true;
                for (int y = 0; y < result.Length; y++)
                {
                    for (int x = 0; x < result[y].Length; x++)
                    {
                        for (int i = -1; i <= 1 && result[y][x] == 'X'; i++)
                        {
                            for (int j = -1; j <= 1 && result[y][x] == 'X'; j++)
                            {
                                var tempY = y + i;
                                var tempX = x + j;
                                if (tempY < 0 || tempY > result.Length - 1 ||
                                    tempX < 0 || tempX > result[y].Length - 1 ||
                                    result[tempY][tempX] == ' ')
                                {
                                    result[y][x] = ' ';
                                    ready = false;
                                }
                            }
                        }
                    }
                }
            }

            var count = 0;
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (mainPipes.Contains((x, y)))
                    {
                        Console.Write(GetPrintSymbol(lines[y][x], startSymbol));
                    }
                    else
                    {
                        var symbol = result[y * 3 + 1][x * 3 + 1];
                        count += symbol == 'X' ? 1 : 0;
                        Console.Write(symbol);
                    }
                }
                Console.WriteLine();
            }

            return count;
        }

        private List<(int x, int y)> Points(string[] lines, out char startSymbol)
        {
            (int x, int y) start = (0, 0);
            char[][] map = new char[lines.Length][];
            for (int y = 0; y < lines.Length; y++)
            {
                var row = lines[y].ToCharArray();
                map[y] = row;
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (row[x] == 'S')
                    {
                        start = (x, y);
                    }
                }
            }
            List<(int x, int y)> points = [start];
            var currentPoints = GetStartingPoints(map, start, out startSymbol);
            var previousPoints = (start, start);
            while (currentPoints.a != currentPoints.b)
            {
                points.AddRange([currentPoints.a, currentPoints.b]);
                var next = (GetNextPosition(map, currentPoints.a, previousPoints.Item1), GetNextPosition(map, currentPoints.b, previousPoints.Item2));
                previousPoints = currentPoints;
                currentPoints = next;
            }
            return [.. points, currentPoints.a];
        }        

        private static ((int x, int y) a, (int x, int y) b) GetStartingPoints(char[][] map, (int x, int y) start, out char startSymbol)
        {
            (int x, int y) north = (start.x, Math.Max(start.y - 1, 0));
            (int x, int y) east = (Math.Min(start.x + 1, map[start.y].Length - 1), start.y);
            (int x, int y) south = (start.x, Math.Min(start.y + 1, map.Length - 1));
            (int x, int y) west = (Math.Max(start.x - 1, 0), start.y);

            var northPipe = map[north.y][north.x];
            var eastPipe = map[east.y][east.x];
            var southPipe = map[south.y][south.x];
            var westPipe = map[west.y][west.x];

            var optionsNorth = new[] { '|', '7', 'F' };
            var optionsEast = new[] { '-', 'J', '7' };
            var optionsSouth = new[] { '|', 'L', 'J' };
            var optionsWest = new[] { '-', 'L', 'F' };

            if (optionsNorth.Contains(northPipe) && optionsEast.Contains(eastPipe))
            {
                startSymbol = 'L';
                return (north, east);
            }
            if (optionsEast.Contains(eastPipe) && optionsSouth.Contains(southPipe))
            {
                startSymbol = 'F';
                return (east, south);
            }
            if (optionsSouth.Contains(southPipe) && optionsWest.Contains(westPipe))
            {
                startSymbol = '7';
                return (south, west);
            }
            if (optionsWest.Contains(westPipe) && optionsNorth.Contains(northPipe))
            {
                startSymbol = 'J';
                return (west, north);
            }
            if (optionsNorth.Contains(northPipe) && optionsSouth.Contains(southPipe))
            {
                startSymbol = '|';
                return (north, south);
            }
            if (optionsEast.Contains(eastPipe) && optionsWest.Contains(westPipe))
            {
                startSymbol = '-';
                return (east, west);
            }

            throw new NotSupportedException();
        }

        private static (int x, int y) GetNextPosition(char[][] map, (int x, int y) current, (int x, int y) previous)
        {
            var direction = Direction.North;
            if (current.x > previous.x)
            {
                direction = Direction.East;
            }
            else if (current.y > previous.y)
            {
                direction = Direction.South;
            }
            else if (current.x < previous.x)
            {
                direction = Direction.West;
            }
            var north = (current.x, current.y - 1);
            var east = (current.x + 1, current.y);
            var south = (current.x, current.y + 1);
            var west = (current.x - 1, current.y);
            return map[current.y][current.x] switch
            {
                '|' when direction == Direction.North => north,
                '|' => south,
                '-' when direction == Direction.East => east,
                '-' => west,
                'L' when direction == Direction.South => east,
                'L' => north,
                'J' when direction == Direction.East => north,
                'J' => west,
                '7' when direction == Direction.East => south,
                '7' => west,
                'F' when direction == Direction.North => east,
                'F' => south,
                _ => throw new NotSupportedException()
            };
        }
    }
}
