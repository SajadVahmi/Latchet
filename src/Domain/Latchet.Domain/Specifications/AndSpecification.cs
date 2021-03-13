using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Specifications
{
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        public AndSpecification(ISpecification<T> right, ISpecification<T> left) : base(right, left)
        {
        }

        public override bool IsSatisfiedBy(T value)
        {
            return Right.IsSatisfiedBy(value) &&
                   Left.IsSatisfiedBy(value);
        }
    }
}
