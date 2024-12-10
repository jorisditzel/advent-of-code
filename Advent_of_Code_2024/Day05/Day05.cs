namespace Advent_of_Code_2024
{
    public class Day05 : IDay
    {
        public long PartOne(string[] lines)
        {
            var rules = new List<(int left, int right)>();
            var updates = new List<List<int>>();

            var enumerator = lines.GetEnumerator();
            while (enumerator.MoveNext() && !string.IsNullOrWhiteSpace((string)enumerator.Current))
            {
                var rule = ((string)enumerator.Current).Split("|");
                rules.Add((int.Parse(rule[0]), int.Parse(rule[1])));
            }
            while (enumerator.MoveNext())
            {
                updates.Add(((string)enumerator.Current).Split(",").Select(int.Parse).ToList());
            }

            var result = 0L;
            foreach (var update in updates)
            {
                var valid = true;
                foreach (var rule in rules)
                {
                    var indexLeft = update.IndexOf(rule.left);
                    var indexRight = update.IndexOf(rule.right);
                    if (indexLeft != -1 && indexRight != -1 && indexLeft > indexRight)
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                {
                    result += update[update.Count / 2];
                }
            }
            return result;
        }

        public long PartTwo(string[] lines)
        {
            var rules = new List<(int left, int right)>();
            var updates = new List<List<int>>();

            var enumerator = lines.GetEnumerator();
            while (enumerator.MoveNext() && !string.IsNullOrWhiteSpace((string)enumerator.Current))
            {
                var rule = ((string)enumerator.Current).Split("|");
                rules.Add((int.Parse(rule[0]), int.Parse(rule[1])));
            }
            while (enumerator.MoveNext())
            {
                updates.Add(((string)enumerator.Current).Split(",").Select(int.Parse).ToList());
            }

            var result = 0L;
            foreach (var update in updates)
            {
                var updated = false;
                var valid = false;
                while (!valid)
                {
                    valid = true;
                    foreach (var rule in rules)
                    {
                        var indexLeft = update.IndexOf(rule.left);
                        var indexRight = update.IndexOf(rule.right);
                        if (indexLeft != -1 && indexRight != -1 && indexLeft > indexRight)
                        {
                            var value = update[indexRight];
                            update.RemoveAt(indexRight);
                            update.Insert(indexLeft, value);
                            updated = true;
                            valid = false;
                            //opnieuw de regels door
                            break;
                        }
                    }
                }
                if (updated)
                {
                    result += update[update.Count / 2];
                }
            }
            return result;
        }
    }
}
