using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Contracts.Services.JsonSerializer
{
    public interface IJsonSerializer
    {
        string Serilize<TInput>(TInput input);
        TOutput Deserialize<TOutput>(string input);
        object Deserialize(string input, Type type);
    }
}
