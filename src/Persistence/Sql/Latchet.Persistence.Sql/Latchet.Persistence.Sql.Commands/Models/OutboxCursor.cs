using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Persistence.Sql.Commands.Models
{
    public class OutboxCursor
    {
        public long Position { get; set; }
    }
}
