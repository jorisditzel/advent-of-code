namespace Advent_of_Code_2023
{
    public record Bag(int Red, int Green, int Blue)
    {
        public static implicit operator int(Bag bag) => bag.Red * bag.Green * bag.Blue;

        public static bool operator <(Bag a, Bag b) =>
            a.Red < b.Red && a.Green < b.Green && a.Blue < b.Blue;

        public static bool operator >(Bag a, Bag b) =>
            a.Red > b.Red && a.Green > b.Green && a.Blue > b.Blue;

        public static bool operator <=(Bag a, Bag b) =>
            a.Red <= b.Red && a.Green <= b.Green && a.Blue <= b.Blue;

        public static bool operator >=(Bag a, Bag b) =>
            a.Red >= b.Red && a.Green >= b.Green && a.Blue >= b.Blue;

        public Bag(string[] reveals) : this(Parse(reveals))
        {
        }

        private static Bag Parse(string[] reveals)
        {
            var red = 0;
            var green = 0;
            var blue = 0;
            foreach (var reveal in reveals)
            {
                foreach (var item in reveal.Split(','))
                {
                    var countColor = item.Trim().Split(' ');
                    var count = int.Parse(countColor[0]);
                    var color = countColor[1];
                    switch (color)
                    {
                        case "red":
                            red = Math.Max(count, red);
                            break;
                        case "green":
                            green = Math.Max(count, green);
                            break;
                        case "blue":
                            blue = Math.Max(count, blue);
                            break;
                    }
                }
            }
            return new Bag(red, green, blue);
        }
    }
}
