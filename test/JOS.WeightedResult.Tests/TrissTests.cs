using System.Collections.Generic;
using Xunit;

namespace JOS.WeightedResult.Tests
{
    public class TrissTests
    {
        [Fact]
        public void Triss_Example_For_Alex()
        {
            var numberOfTickets = 12_000_000;
            var totalNumberOfWins = 2_574_000;
            var totalNumberOfNonWins = numberOfTickets - totalNumberOfWins;
            var prices = new List<ProbabilityItem<int>>
            {
                new ProbabilityItem<int>(totalNumberOfNonWins, 0),
                new ProbabilityItem<int>(1_202_118, 30, "30 SEK"),
                new ProbabilityItem<int>(1131114, 60, "60 SEK"),
                new ProbabilityItem<int>(156000, 90, "90 SEK"),
                new ProbabilityItem<int>(43200, 120, "120 SEK"),
                new ProbabilityItem<int>(22560, 150, "150 SEK"),
                new ProbabilityItem<int>(7200, 180, "180 SEK"),
                new ProbabilityItem<int>(5580, 300, "300 SEK"),
                new ProbabilityItem<int>(1200, 600, "600 SEK"),
                new ProbabilityItem<int>(1200, 500, "500 SEK"),
                new ProbabilityItem<int>(960, 1_000, "1000 SEK"),
                new ProbabilityItem<int>(750, 450, "450 SEK"),
                new ProbabilityItem<int>(480, 1_500, "1500 SEK"),
                new ProbabilityItem<int>(360, 900, "900 SEK"),
                new ProbabilityItem<int>(300, 750, "750 SEK"),
                new ProbabilityItem<int>(300, 2_000, "2000 SEK"),
                new ProbabilityItem<int>(276, 10_000, "10 000 SEK"),
                new ProbabilityItem<int>(180, 5_000, "5000 SEK"),
                new ProbabilityItem<int>(90, 2_500, "2500 SEK"),
                new ProbabilityItem<int>(48, 20_000, "20 000 SEK"),
                new ProbabilityItem<int>(36, 265_000, "265 000 SEK"),
                new ProbabilityItem<int>(18, 50_000, "50 000 SEK"),
                new ProbabilityItem<int>(12, 100_000, "100 000 SEK"),
                new ProbabilityItem<int>(6, 200_000, "200 000 SEK"),
                new ProbabilityItem<int>(6, 1_000_000, "1 000 000 SEK"),
                new ProbabilityItem<int>(6, 2_765_000, "2 765 000 SEK")
            };
            var query = new WeightedResultQuery<int>(prices);
            var results = new Dictionary<int, int>();

            for (int i = 0; i < numberOfTickets; i++)
            {
                var result = query.Execute();
                if (!results.ContainsKey(result))
                {
                    results.Add(result, 0);
                }

                results[result]++;
            }
        }
    }
}
