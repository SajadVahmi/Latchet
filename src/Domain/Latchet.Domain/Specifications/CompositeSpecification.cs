using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Specifications
{
    public abstract class CompositeSpecification<T> : Specification<T>
    {
        public ISpecification<T> Right { get; private set; }
        public ISpecification<T> Left { get; private set; }
        protected CompositeSpecification(ISpecification<T> right, ISpecification<T> left)
        {
            Right = right;
            Left = left;
        }
    }
}
