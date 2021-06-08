using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Application.Common
{
    public abstract class Result : IResult
    {
        protected readonly List<string> messages = new List<string>();

        public IEnumerable<string> Messages => messages;

        public ResultStatus Status { get; set; }

        public void AddMessage(string message)
        {
            messages.Add(message);
        }

        public void ClearMessages()
        {
            messages.Clear();
        }
    }
}
