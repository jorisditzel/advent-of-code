namespace Advent_of_Code_2023.Test
{
    [TestFixture]
    public abstract class TestBase<T> where T : IDay, new()
    {
        public abstract long AnswerPartOne { get; }

        public virtual string InputPartOne => "input.txt";

        public abstract long AnswerPartTwo { get; }

        public virtual string InputPartTwo => "input.txt";


        [Test]
        public async Task PartOne()
        {
            var input = await File.ReadAllLinesAsync(Path.Combine(typeof(T).Name, InputPartOne));
            var result = new T().PartOne(input);
            Assert.That(result, Is.EqualTo(AnswerPartOne));
        }

        [Test]
        public async Task PartTwo()
        {
            var input = await File.ReadAllLinesAsync(Path.Combine(typeof(T).Name, InputPartTwo));
            var result = new T().PartTwo(input);
            Assert.That(result, Is.EqualTo(AnswerPartTwo));
        }
    }
}
