using Latchet.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Application.Commands
{
    /// <summary>
    /// نتیجه انجام هر عملیات به کمک این کلاس بازگشت داده می‌شود.
    /// دلایل استفاده از این الگو و پیاده سازی کاملی از این الگو را در لینک زیر می‌توانید مشاهده کنید
    /// https://github.com/vkhorikov/CqrsInPractice
    /// </summary>
    public class CommandResult : Result
    {

    }

    public class CommandResult<TData> : CommandResult
    {
        internal TData data;
        public TData Data
        {
            get
            {
                return data;
            }
        }

    }
}
