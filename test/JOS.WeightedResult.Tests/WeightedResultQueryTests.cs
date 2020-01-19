using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JOS.WeightedResult.Tests
{
    public class WeightedResultQueryTests
    {
        private readonly WeightedResultQuery<string> _sut;

        public WeightedResultQueryTests()
        {
            var data = new List<(int, string)>
            {
                (1, "One"),
                (2, "Two"),
                (7, "Seven"),
            };
            _sut = new WeightedResultQuery<string>(data);
        }

        [Fact]
        public void Hej()
        {
            var results = new List<string>();

            for (var i = 0; i < 10000; i++)
            {
                var result = _sut.Execute();
                results.Add(result);
            }

            var first = results.Count(x => x == "One");
            var second = results.Count(x => x == "Two");
            var third = results.Count(x => x == "Seven");
        }
    }
}
