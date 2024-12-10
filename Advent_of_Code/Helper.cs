using System.Data;
using System.Security.Cryptography;

namespace Advent_of_Code
{
    public static class Helper
    {
        public static long Gcd(long first, long second) => second == 0 ? first : Gcd(second, first % second);

        public static long Lcm(long first, long second) => first * second / Gcd(first, second);

        public static char[][] ToGrid(this string[] stringArray)
        {
            return stringArray.Select(s => s.ToArray()).ToArray();
        }

        public static char[][] Transpose(this char[][] map)
        {
            var result = new char[map[0].Length][];
            for (int y = 0; y < map[0].Length; y++)
            {
                result[y] = new char[map.Length];
                for (int x = 0; x < map.Length; x++)
                {
                    result[y][x] = map[x][y];
                }
            }
            return result;
        }

        public static char[][] RotateClockwise(this char[][] map) => map.Transpose().Select(row => row.Reverse().ToArray()).ToArray();

        public static string Hash(this char[][] map)
        {
            byte[] hash = SHA256.HashData(map.SelectMany(r => r.Select(c => (byte)c)).ToArray());
            return Convert.ToBase64String(hash);
        }

        public static void Print(this char[][] map)
        {
            foreach (var x in map)
            {
                Console.WriteLine(x);
            }
        }
    }
}
