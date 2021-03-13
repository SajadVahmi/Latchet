using Latchet.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Entities
{
    public class Entity:IAuditable
    {
        public Id Id { get; set; }
    }
}
