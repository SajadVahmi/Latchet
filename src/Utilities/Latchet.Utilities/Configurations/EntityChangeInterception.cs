using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Utilities.Configurations
{
    public class EntityChangeInterception
    {
        public bool Enabled { get; set; }
        public string EntityChageInterceptorRepositoryTypeName { get; set; }
    }
}
