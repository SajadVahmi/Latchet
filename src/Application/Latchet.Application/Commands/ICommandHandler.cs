using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Application.Commands
{
    public interface ICommandHandler<TCommand, TData> where TCommand : ICommand<TData>
    {
        Task<CommandResult<TData>> Handle(TCommand request);
    }
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<CommandResult> Handle(TCommand request);
    }
}
