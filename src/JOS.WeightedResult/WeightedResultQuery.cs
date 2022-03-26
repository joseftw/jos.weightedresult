using System;
using System.Collections.Generic;
using System.Linq;

namespace JOS.WeightedResult
{
    public class WeightedResultQuery<T>
    {
        private readonly T[] _values;
        private readonly AliasMethodVose _aliasMethodVose;

        public WeightedResultQuery(
            IReadOnlyCollection<ProbabilityItem<T>> values)
        {
            _values = values.Select(x => x.Value).ToArray();
            _aliasMethodVose = new AliasMethodVose(values.Select(x => x.Probability));
            decimal sum = values.Sum(x => x.Probability);
            Probabilities = values.Select((x, i) =>
            {
                var percentage = x.Probability / sum * 100;
                return (percentage, !string.IsNullOrWhiteSpace(x.Description) ? x.Description : $"Item {++i}");
            }).OrderByDescending(x => x.percentage).ToList();
        }

        public T Execute()
        {
            var index = _aliasMethodVose.NextValue();
            return _values[index];
        }

        public IReadOnlyCollection<(decimal Percentage, string Description)> Probabilities { get; }
    }
}
