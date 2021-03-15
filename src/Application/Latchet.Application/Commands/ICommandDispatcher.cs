using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Application.Commands
{
    public interface ICommandDispatcher
    {
        Task<CommandResult> Send<TCommand>(in TCommand command) where TCommand : class, ICommand;
        Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command) where TCommand : class, ICommand<TData>;

    }
}
