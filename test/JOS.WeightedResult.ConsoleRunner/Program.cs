using System;
using System.Collections.Generic;

namespace JOS.WeightedResult.ConsoleRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var data = new List<ProbabilityItem<string>>
            {
                new(1, "One", "One"),
                new(2, "Two", "Two"),
                new(7, "Seven", "Seven")
            };

            var query = new WeightedResultQuery<string>(data);
            for (var i = 0; i < 100; i++)
            {
                Console.WriteLine(query.Execute());
            }
        }
    }
}
