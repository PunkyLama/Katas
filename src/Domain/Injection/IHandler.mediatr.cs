using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Injection
{
    public interface IHandler <in TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}
