using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Utilities.Configurations
{
    public class LatchetSwaggerConfiguration
    {
        public bool Enabled { get; set; }
        public Swaggerdoc SwaggerDoc { get; set; }
    }

    public class Swaggerdoc
    {
        public string Version { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
    }
}
