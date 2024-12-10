namespace Advent_of_Code_2023
{
    public class Day05 : IDay
    {
        public long PartOne(string[] lines)
        {
            var map = BuildMapCollections(lines, out var seeds);
            return seeds.Aggregate(long.MaxValue, (min, seed) =>
            {
                var first = map.SeedToLocation(seed.Number);
                var second = map.SeedToLocation(seed.Range);
                return new long[] { first, second, min }.Min();
            });
        }

        public long PartTwo(string[] lines)
        {
            var map = BuildMapCollections(lines, out var seeds);

            Parallel.ForEach(seeds, seed =>
            {
                var min = long.MaxValue;
                var percentage = 0;
                for (var i = 0; i < seed.Range; i++)
                {
                    var location = map.SeedToLocation(seed.Number + i);
                    min = Math.Min(location, min);

                    if ((long)(i / (double)seed.Range * 100) > percentage)
                    {
                        percentage++;
                        Console.WriteLine($"Seed {seed.Number}: {percentage}%");
                    }
                }
                seed.Location = min;
            });

            return seeds.Min(s => s.Location);
        }

        private MapCollections BuildMapCollections(string[] lines, out List<Seed> seeds)
        {
            var maps = new MapCollections();
            seeds = [];
            List<Map> currentMap = [];
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                if (line.StartsWith("seeds"))
                {
                    var seedRanges = line.Split(": ")[1].Split(' ').Select(long.Parse).ToList();
                    for (var i = 0; i < seedRanges.Count; i += 2)
                    {
                        seeds.Add(new Seed(seedRanges[i], seedRanges[i + 1]));
                    }
                    continue;
                }
                if (line.Contains("map"))
                {
                    var category = line.Split(' ')[0];
                    currentMap = [];
                    maps.Maps.Add(currentMap);
                    continue;
                }
                var mapping = line.Split(' ');
                var source = long.Parse(mapping[1]);
                var destination = long.Parse(mapping[0]);
                var range = long.Parse(mapping[2]);
                currentMap.Add(new Map(source, destination, range));
            }

            return maps;
        }

        private record Seed(long Number, long Range)
        {
            public long Location { get; set; }
        };

        private record Map(long Source, long Destination, long Range);

        private class MapCollections
        {
            public List<List<Map>> Maps { get; set; } = [];
            private long Destination(long source, List<Map> maps)
            {
                var map = maps.SingleOrDefault(m => source >= m.Source && source < m.Source + m.Range);
                if (map == null)
                {
                    return source;
                }
                var offset = source - map.Source;
                return map.Destination + offset;
            }

            public long SeedToLocation(long seed) => Maps.Aggregate(seed, (source, map) => source = Destination(source, map));
        }
    }
}
