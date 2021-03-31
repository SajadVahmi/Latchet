using Latchet.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.ValueObjects
{
    public record Range<T> : ValueObject<Range<T>> where T : IComparable<T>
    {
        public T Minimum { get; private set; }
        public T Maximum { get; private set; }
        public Range(T minimum, T maximum)
        {
            if (minimum.CompareTo(maximum) > 0) throw new InvalidValueObjectStateException("Minimum must be less than the maximum", nameof(minimum),nameof(maximum));

            Minimum = minimum;
            Maximum = maximum;
        }
        private Range(){ }
        public bool IsOverlapWith(Range<T> range)
        {
            return this.Minimum.CompareTo(range.Maximum) < 0 && range.Minimum.CompareTo(this.Maximum) < 0;
        }
        public bool Contains(T value)
        {
            return (Minimum.CompareTo(value) <= 0) && (value.CompareTo(Maximum) <= 0);
        }
    }
}
