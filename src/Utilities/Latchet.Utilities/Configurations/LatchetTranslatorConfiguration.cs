using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Utilities.Configurations
{
    
    public class LatchetTranslatorConfiguration
    {
        public string CultureInfo { get; set; } = "en-Us";
        public string TranslatorTypeName { get; set; }
        public Parrottranslator Parrottranslator { get; set; }
    }


    public class Parrottranslator
    {
        public string ConnectionString { get; set; }
        public bool AutoCreateSqlTable { get; set; } = true;
        public string TableName { get; set; } = "ParrotTranslations";
        public string SchemaName { get; set; } = "dbo";
    }
}
