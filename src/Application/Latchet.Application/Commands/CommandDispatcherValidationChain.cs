using FluentValidation;
using Latchet.Application.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Application.Commands
{
    public class CommandDispatcherValidationChain : CommandDispatcherChain
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public CommandDispatcherValidationChain(CommandDispatcherDomainExceptionHandlerChain commandDispatcher, IServiceScopeFactory serviceScopeFactory) : base(commandDispatcher)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public override Task<CommandResult> Send<TCommand>(in TCommand command)
        {
            using var serviceScope = serviceScopeFactory.CreateScope();
            var validationResult = Validate<TCommand, CommandResult>(command, serviceScope);
            if (validationResult != null)
            {
                return Task.FromResult(validationResult);
            }
            return commandDispatcher.Send(command);
        }

        public override Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command)
        {
            using var serviceScope = serviceScopeFactory.CreateScope();
            var validationResult = Validate<TCommand, CommandResult<TData>>(command, serviceScope);
            if (validationResult != null)
            {
                return Task.FromResult(validationResult);
            }
            return commandDispatcher.Send<TCommand, TData>(command);
        }

        private static TValidationResult Validate<TCommand, TValidationResult>(TCommand command, IServiceScope serviceScope) where TValidationResult : ApplicationServiceResult, new()
        {
            var validator = serviceScope.ServiceProvider.GetService<IValidator<TCommand>>();
            if (validator != null)
            {
                var validationResult = validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    TValidationResult res = new TValidationResult();
                    res.Status = ApplicationServiceStatus.ValidationError;
                    foreach (var item in validationResult.Errors)
                    {
                        res.AddMessage(item.ErrorMessage);
                    }

                    return res;
                }
            }
            return null;
        }
    }
}
