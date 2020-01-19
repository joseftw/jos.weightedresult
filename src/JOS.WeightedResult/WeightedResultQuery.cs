using System.Collections.Generic;
using System.Linq;

namespace JOS.WeightedResult
{
    public class WeightedResultQuery<T>
    {
        private readonly AliasMethodVose _aliasMethodVose;
        private readonly T[] _values;

        public WeightedResultQuery(IReadOnlyCollection<(int Probability, T Value)> values)
        {
            _aliasMethodVose = new AliasMethodVose(values.Select(x => x.Probability));
            _values = values.Select(x => x.Value).ToArray();
        }

        public T Execute()
        {
            var index = _aliasMethodVose.NextValue();
            return _values[index];
        }
    }
}
