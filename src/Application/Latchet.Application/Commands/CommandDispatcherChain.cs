using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Application.Commands
{
    public abstract class CommandDispatcherChain : ICommandDispatcher
    {
        protected ICommandDispatcher commandDispatcher;
        public CommandDispatcherChain(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }
        public abstract Task<CommandResult> Send<TCommand>(in TCommand command) where TCommand : class, ICommand;

        public abstract Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command) where TCommand : class, ICommand<TData>;
    }
}
