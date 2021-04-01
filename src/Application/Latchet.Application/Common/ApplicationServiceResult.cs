using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Application.Common
{
    public abstract class ApplicationServiceResult : IApplicationServiceResult
    {
        protected readonly List<string> messages = new List<string>();

        public IEnumerable<string> Messages => messages;

        public ApplicationServiceStatus Status { get; set; }

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
