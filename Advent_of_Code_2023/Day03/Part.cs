namespace Advent_of_Code_2023
{
    public record Part(char Symbol, int X, int Y)
    {
        private readonly List<int> numbers = [];

        public void AddNumber(int Number)
        {
            numbers.Add(Number);
        }

        public int Sum() => numbers.Sum();

        public int GearRatio() => IsGear() ? Multiply() : 0;

        private int Multiply() => numbers.Aggregate((x, y) => x * y);

        private bool IsGear() => Symbol == '*' && numbers.Count > 1;

        public virtual bool Equals(Part other) => Symbol == other?.Symbol && X == other?.X && Y == other?.Y;

        public override int GetHashCode()
        {
            return HashCode.Combine(Symbol, X, Y);
        }
    }
}
