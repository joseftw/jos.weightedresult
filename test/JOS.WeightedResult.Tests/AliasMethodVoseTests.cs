using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;

namespace JOS.WeightedResult.Tests
{
    public class AliasMethodVoseTests
    {
        private const double MaxDeviation = 0.65;
        private const int Runs = 100;
        private const int Invocations = 1_000_00;

        [Fact]
        public void IfAnyProbabilityIsLessThen0_ThenThrowsArgumentException()
        {
            var probabilities = new List<int> {1, 2, 3, -4};

            var exception = Should.Throw<ArgumentException>(() => new AliasMethodVose(probabilities));

            exception.Message.ShouldBe("Probability must be equal to 0 or higher, was '-4'");
        }

        [Theory]
        [InlineData(9, 1)]
        [InlineData(8, 2)]
        [InlineData(7, 3)]
        [InlineData(6, 4)]
        [InlineData(5, 5)]
        [InlineData(4, 6)]
        [InlineData(3, 7)]
        [InlineData(2, 8)]
        [InlineData(1, 9)]
        [InlineData(25, 75)]
        public void GivenTwoProbabilities_ThenResultDoesNotDeviateMoreThanTheAcceptedDeviation(
            int firstProbability,
            int secondProbability)
        {
            var probabilities = new List<int> { firstProbability, secondProbability };
            double totalSum = probabilities.Sum();
            var sut = new AliasMethodVose(probabilities);
            var received = new List<int>(Invocations);

            for (var i = 0; i < Runs; i++)
            {
                for (var j = 0; j < Invocations; j++)
                {
                    var result = sut.NextValue();
                    received.Add(result);
                }

                var first = received.Count(x => x == 0) / (double)received.Count;
                var second = received.Count(x => x == 1) / (double)received.Count;

                first.ShouldBeInRange(GetExpectedMin(firstProbability, totalSum), GetExpectedMax(firstProbability, totalSum));
                second.ShouldBeInRange(GetExpectedMin(secondProbability, totalSum), GetExpectedMax(secondProbability, totalSum));
                received.Clear();
            }
        }

        [Theory]
        [InlineData(8, 1, 1)]
        [InlineData(7, 2, 1)]
        [InlineData(6, 3, 1)]
        [InlineData(5, 4, 1)]
        [InlineData(5, 5, 0)]
        [InlineData(4, 5, 1)]
        [InlineData(3, 6, 1)]
        [InlineData(2, 7, 1)]
        [InlineData(1, 8, 1)]
        public void GivenThreeProbabilities_ThenResultDoesNotDeviateMoreThanTheAcceptedDeviation(
            int firstProbability,
            int secondProbability,
            int thirdProbability
            )
        {
            var probabilities = new List<int> { firstProbability, secondProbability, thirdProbability };
            double totalSum = probabilities.Sum();
            var sut = new AliasMethodVose(probabilities);
            var received = new List<int>(Invocations);

            for (var i = 0; i < Runs; i++)
            {
                for (var j = 0; j < Invocations; j++)
                {
                    var result = sut.NextValue();
                    received.Add(result);
                }

                var first = received.Count(x => x == 0) / (double)received.Count;
                var second = received.Count(x => x == 1) / (double)received.Count;
                var third = received.Count(x => x == 2) / (double)received.Count;
                first.ShouldBeInRange(GetExpectedMin(firstProbability, totalSum), GetExpectedMax(firstProbability, totalSum));
                second.ShouldBeInRange(GetExpectedMin(secondProbability, totalSum), GetExpectedMax(secondProbability, totalSum));
                third.ShouldBeInRange(GetExpectedMin(thirdProbability, totalSum), GetExpectedMax(thirdProbability, totalSum));
                received.Clear();
            }
        }

        private static double GetExpectedMin(int probability, double totalSum)
        {
            return GetExpected(probability, totalSum, -MaxDeviation);
        }

        private static double GetExpectedMax(int probability, double totalSum)
        {
            return GetExpected(probability, totalSum, MaxDeviation);
        }

        private static double GetExpected(int probability, double totalSum, double deviation)
        {
            return probability / totalSum + deviation;
        }
    }
}
