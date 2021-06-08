using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Application.Common
{
    public interface IResult
    {
        IEnumerable<string> Messages { get; }
        ResultStatus Status { get; set; }
    }
}
