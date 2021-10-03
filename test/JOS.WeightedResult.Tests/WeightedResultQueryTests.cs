using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace JOS.WeightedResult.Tests
{
    public class WeightedResultQueryTests
    {
        [Fact]
        public void ShouldDisplayProbabilitiesInPercentageCorrectly()
        {
            var values = new List<ProbabilityItem<string>>
            {
                new ProbabilityItem<string>(1, "template-1", "Template 1"),
                new ProbabilityItem<string>(5, "template-2", "Template 2"),
                new ProbabilityItem<string>(4, "template-3", "Template 3")
            };

            var sut = new WeightedResultQuery<string>(values);

            sut.Probabilities.Count.ShouldBe(3);
            sut.Probabilities.ShouldContain(x => x.Percentage.Equals(10) && x.Description == "Template 1");
            sut.Probabilities.ShouldContain(x => x.Percentage.Equals(50) && x.Description == "Template 2");
            sut.Probabilities.ShouldContain(x => x.Percentage.Equals(40) && x.Description == "Template 3");
        }

        [Fact]
        public void ShouldDisplayProbabilitiesInPercentageCorrectly_WithDecimals()
        {
            var values = new List<ProbabilityItem<string>>
            {
                new ProbabilityItem<string>(3, "template-1", "Template 1"),
                new ProbabilityItem<string>(3, "template-2", "Template 2"),
                new ProbabilityItem<string>(3, "template-3", "Template 3")
            };

            var sut = new WeightedResultQuery<string>(values);

            sut.Probabilities.Count.ShouldBe(3);
            sut.Probabilities.ShouldContain(x => x.Percentage.Equals(33.33) && x.Description == "Template 1");
            sut.Probabilities.ShouldContain(x => x.Percentage.Equals(33.33) && x.Description == "Template 2");
            sut.Probabilities.ShouldContain(x => x.Percentage.Equals(33.33) && x.Description == "Template 3");
        }
    }
}
