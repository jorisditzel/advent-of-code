using Advent_of_Code;
using System.Diagnostics;
using System.Reflection;

public abstract class Program<T> where T : IDay
{
    public async Task Run()
    {
        var days = Assembly.GetAssembly(typeof(T)).GetTypes().Where(typeof(T).IsAssignableFrom);

        while (true)
        {
            Console.Clear();
            Console.Write("Day (default today): ");
            var dayInput = Console.ReadLine();
            var day = string.IsNullOrWhiteSpace(dayInput) ? DateTime.Today.Day : int.Parse(dayInput);
            var type = days.SingleOrDefault(d => d.Name == $"Day{day:D2}");
            if (type == null)
            {
                Console.WriteLine("Day {day} does not exist!");
                Console.ReadKey();
                continue;
            }
            var instance = (T)Activator.CreateInstance(type);
            var lines = await File.ReadAllLinesAsync(Path.Combine(type.Name, "input.txt"));

            var stopwatch = Stopwatch.StartNew();
            var partOne = instance.PartOne(lines);
            Console.WriteLine($"Part 1: {partOne} ({stopwatch.ElapsedMilliseconds}ms)");
            stopwatch.Restart();
            var partTwo = instance.PartTwo(lines);
            Console.WriteLine($"Part 2: {partTwo} ({stopwatch.ElapsedMilliseconds}ms)");
            Console.ReadKey();
        }
    }
}

