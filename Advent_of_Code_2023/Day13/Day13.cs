using Advent_of_Code;

namespace Advent_of_Code_2023
{
    public class Day13 : IDay
    {
        public long PartOne(string[] lines)
        {
            var patterns = Patterns(lines).Select(p => p.ToArray()).ToArray();
            return patterns.Aggregate(0, (count, pattern) => count + FindMirror(pattern));
        }

        private static int FindMirror(string[] pattern)
        {
            var mirror = HorizontalMirror(pattern);
            if (mirror != 0)
            {
                return mirror * 100;
            }
            var transpose = pattern.Select(p => p.ToCharArray()).ToArray().Transpose();
            mirror = HorizontalMirror(transpose.Select(t => new string(t)).ToArray());
            return mirror;
        }

        public long PartTwo(string[] lines)
        {
            var patterns = Patterns(lines).Select(p => p.ToArray()).ToArray();
            var count = 0;
            var i = 0;
            foreach (var pattern in patterns)
            {
                var old = FindMirror(pattern);
                var mirror = MirrorSmudge(pattern);
                if (mirror != 0)
                {
                    Console.WriteLine($"Pattern {i++}: {mirror * 100} (old {old})");
                    count += mirror * 100;
                    continue;
                }
                var transpose = pattern.Select(p => p.ToCharArray()).ToArray().Transpose();
                mirror = MirrorSmudge(transpose.Select(t => new string(t)).ToArray());
                count += mirror;
                Console.WriteLine($"Pattern {i++}: {mirror} (old {old})");
            }
            return count;
        }

        private static bool LineEquals(string a, string b, out bool hasSmudge)
        {
            hasSmudge = false;
            var count = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    count++;
                }
            }
            if (count > 0)
            {
                hasSmudge = true;
            }
            return count <= 1;
        }

        private static int MirrorSmudge(string[] pattern)
        {
            var oldMirror = HorizontalMirror(pattern);
            int mirror = 0;
            bool smudged = false;
            for (var i = 1; i < pattern.Length; i++)
            {
                if ((oldMirror == 0 || oldMirror != i) && mirror == 0 && LineEquals(pattern[i], pattern[i - 1], out smudged))
                {
                    mirror = i;
                }
                else if (mirror != 0)
                {
                    if (mirror - (i - mirror) - 1 < 0)
                    {
                        break;
                    }
                    if (!LineEquals(pattern[i], pattern[mirror - (i - mirror) - 1], out var hasSmudge) ||
                        (smudged && hasSmudge))
                    {
                        if(LineEquals(pattern[i], pattern[i - 1], out smudged))
                        {
                            mirror = i;
                        }
                        else
                        {
                            mirror = 0;
                        }                    
                    }
                }
            }

            return mirror;
        }

        private static int HorizontalMirror(string[] pattern)
        {
            int mirror = 0;
            for (var i = 1; i < pattern.Length; i++)
            {
                if (mirror == 0 && pattern[i] == pattern[i - 1])
                {
                    mirror = i;
                }
                else if (mirror != 0)
                {
                    if (mirror - (i - mirror) - 1 < 0)
                    {
                        break;
                    }
                    if (pattern[i] != pattern[mirror - (i - mirror) - 1])
                    {
                        mirror = 0;
                    }
                }
            }

            return mirror;
        }

        private static IEnumerable<List<string>> Patterns(string[] lines)
        {
            List<string> pattern = [];
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    yield return pattern;
                    pattern = [];
                }
                else
                {
                    pattern.Add(line);
                }
            }
            yield return pattern;
        }
    }
}
