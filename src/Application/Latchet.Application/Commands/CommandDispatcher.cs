using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Application.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task<CommandResult> Send<TCommand>(in TCommand command) where TCommand : class, ICommand
        {
            var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            return handler.Handle(command);

        }

        public Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command) where TCommand : class, ICommand<TData>
        {
            var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TData>>();
            return handler.Handle(command);
        }

    }
}
