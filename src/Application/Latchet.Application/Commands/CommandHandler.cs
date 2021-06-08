using Latchet.Application.Common;
using Latchet.Domain.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Application.Commands
{
    public abstract class CommandHandler<TCommand, TData> : ICommandHandler<TCommand, TData>
     where TCommand : ICommand<TData>
    {
        protected readonly LatchetServices latchetServices;
        protected readonly CommandResult<TData> result = new CommandResult<TData>();

        public CommandHandler(LatchetServices latchetServices)
        {
            this.latchetServices = latchetServices;
        }
        public abstract Task<CommandResult<TData>> Handle(TCommand request);
        protected virtual Task<CommandResult<TData>> OkAsync(TData data)
        {
            result.data = data;
            result.Status = ResultStatus.Ok;
            return Task.FromResult(result);
        }
        protected virtual CommandResult<TData> Ok(TData data)
        {
            result.data = data;
            result.Status = ResultStatus.Ok;
            return result;
        }
        protected virtual Task<CommandResult<TData>> ResultAsync(TData data, ResultStatus status)
        {
            result.data = data;
            result.Status = status;
            return Task.FromResult(result);
        }
        protected virtual CommandResult<TData> Result(TData data, ResultStatus status)
        {
            result.data = data;
            result.Status = status;
            return result;
        }
        protected void AddMessage(string message)
        {
            result.AddMessage(message);
        }
       
    }

    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
    {
        protected readonly LatchetServices latchetServices;
        protected readonly CommandResult result = new CommandResult();
        public CommandHandler(LatchetServices latchetServices)
        {
            this.latchetServices = latchetServices;
        }
        public abstract Task<CommandResult> Handle(TCommand request);

        protected virtual Task<CommandResult> OkAsync()
        {
            result.Status = ResultStatus.Ok;
            return Task.FromResult(result);
        }

        protected virtual CommandResult Ok()
        {
            result.Status = ResultStatus.Ok;
            return result;
        }

        protected virtual Task<CommandResult> ResultAsync(ResultStatus status)
        {
            result.Status = status;
            return Task.FromResult(result);
        }
        protected virtual CommandResult Result(ResultStatus status)
        {
            result.Status = status;
            return result;
        }
        protected void AddMessage(string message)
        {
            result.AddMessage(message);
        }
        
    }
}
