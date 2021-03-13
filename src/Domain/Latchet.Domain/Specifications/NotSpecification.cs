using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Specifications
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _target;
        public NotSpecification(ISpecification<T> target)
        {
            _target = target;
        }
        public override bool IsSatisfiedBy(T value)
        {
            return !_target.IsSatisfiedBy(value);
        }
    }
}
