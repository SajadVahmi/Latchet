using Latchet.Application.Common;
using Latchet.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Application.Commands
{
    public class CommandDispatcherDomainExceptionHandlerDecorator : CommandDispatcherDecorator
    {
        
        public CommandDispatcherDomainExceptionHandlerDecorator(CommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
           
        }

        public override Task<CommandResult> Send<TCommand>(in TCommand command)
        {
            try
            {
                return commandDispatcher.Send(command);
            }
            catch (DomainException ex)
            {

                return DomainExceptionHandling<TCommand, CommandResult>(ex);
            }

        }

        public override Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command)
        {
            try
            {
                return commandDispatcher.Send<TCommand, TData>(command);
            }
            catch (DomainException ex)
            {
                return DomainExceptionHandling<TCommand, CommandResult<TData>>(ex);
            }

        }

        private Task<TCommandResult> DomainExceptionHandling<TCommand, TCommandResult>(DomainException ex) where TCommandResult : ApplicationServiceResult, new()
        {
            var type = typeof(TCommandResult);
            dynamic commandResult = new CommandResult();
            if (type.IsGenericType)
            {
                var d1 = typeof(CommandResult<>);
                var makeme = d1.MakeGenericType(type.GetGenericArguments());
                commandResult = Activator.CreateInstance(makeme);
            }
            else
                commandResult.AddMessage(ex.Message);

            commandResult.Status = ApplicationServiceStatus.DomainException;
            return Task.FromResult(commandResult as TCommandResult);
        }
    }
}
