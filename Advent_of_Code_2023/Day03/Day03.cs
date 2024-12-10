namespace Advent_of_Code_2023
{
    public class Day03 : IDay
    {
        public long PartOne(string[] lines)
        {
            var parts = GetAllParts(lines);
            return parts.Sum(p => p.Sum());
        }

        public long PartTwo(string[] lines)
        {
            var parts = GetAllParts(lines);
            return parts.Sum(p => p.GearRatio());
        }

        private List<Part> GetAllParts(string[] lines)
        {
            List<Part> parts = [];
            char[][] schematic = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                schematic[i] = lines[i].ToCharArray();
            }

            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                Part part = null;
                var value = 0;
                for (int x = 0; x < line.Length; x++)
                {
                    var c = line[x];
                    if (char.IsDigit(c))
                    {
                        value = value * 10 + byte.Parse(c.ToString());
                        part ??= GetSurroundingParts(schematic, y, x, parts).SingleOrDefault();
                    }

                    if (!char.IsDigit(c) || x == line.Length - 1)
                    {
                        if (value != 0 && part != null)
                        {
                            part.AddNumber(value);
                        }
                        value = 0;
                        part = null;
                    }
                }
            }
            return parts;
        }

        private static IEnumerable<Part> GetSurroundingParts(char[][] schematic, int y, int x, List<Part> parts)
        {
            var maxY = schematic.Length - 1;
            var maxX = schematic[y].Length - 1;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    var engineY = Math.Max(Math.Min(y + i, maxY), 0);
                    var engineX = Math.Max(Math.Min(x + j, maxX), 0);
                    var c = schematic[engineY][engineX];
                    if (!char.IsDigit(c) && c != '.')
                    {
                        var part = new Part(c, engineX, engineY);
                        var existing = parts.SingleOrDefault(p => p == part);
                        if (existing == null)
                        {
                            parts.Add(part);
                        }
                        yield return existing ?? part;
                    }
                }
            }
        }
    }
}
