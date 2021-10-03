using System;
using System.Collections.Generic;

namespace JOS.WeightedResult.ConsoleRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var data = new List<(int, string)>
            {
                (1, "One"),
                (2, "Two"),
                (7, "Seven"),
            };

            var query = new WeightedResultQuery<string>(data);
            for (var i = 0; i < 100; i++)
            {
                Console.WriteLine(query.Execute());
            }
        }
    }
}
