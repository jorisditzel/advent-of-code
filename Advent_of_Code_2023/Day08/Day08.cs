using Advent_of_Code;
using System.Runtime.CompilerServices;

namespace Advent_of_Code_2023
{
    public class Day08 : IDay
    {
        public long PartOne(string[] lines)
        {
            string operations = lines[0];
            var instruction = ParseInstructions(lines[2..]).Single(i => i.Name == "AAA");
            return Steps(instruction, operations, "ZZZ");
        }



        public long PartTwo(string[] lines)
        {
            string operations = lines[0];
            var instructions = ParseInstructions(lines[2..]).Where(i => i.Name.EndsWith('A'));
            return instructions.Aggregate(1L, (lcm, instruction) => Helper.Lcm(lcm, Steps(instruction, operations, "Z")));
        }

        private static long Steps(Instruction instruction, string operations, string end)
        {
            var count = 0L;
            while (!instruction.Name.EndsWith(end))
            {
                for (int i = 0; i < operations.Length && !instruction.Name.EndsWith(end); i++)
                {
                    instruction = operations[i] switch
                    {
                        'L' => instruction.Left,
                        'R' => instruction.Right,
                        _ => throw new Exception()
                    };
                    count++;
                }
            }

            return count;
        }

        private static Instruction[] ParseInstructions(string[] lines)
        {
            var instructions = lines.Select(i => new Instruction { Name = i.Split('=', StringSplitOptions.TrimEntries)[0] }).ToArray();
            for (int i = 0; i < instructions.Length; i++)
            {
                var directions = lines[i].Split('=', StringSplitOptions.TrimEntries)[1].Split(',', StringSplitOptions.TrimEntries);
                instructions[i].Left = instructions.Single(i => i.Name == directions[0].Replace("(", ""));
                instructions[i].Right = instructions.Single(i => i.Name == directions[1].Replace(")", ""));
            }
            return instructions;
        }

        private class Instruction
        {
            public string Name { get; set; }
            public Instruction Left { get; set; }
            public Instruction Right { get; set; }

            public override string ToString()
            {
                return Name + ": " + Left.Name + ", " + Right.Name;
            }
        }
    }
}
