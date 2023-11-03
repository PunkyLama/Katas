using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Injection
{
    public class Mediatr: IMediatr
    {
        private readonly Func<Type, object> _serviceResolver;
        private readonly ConcurrentDictionary<Type, Type> _handlerDetails;

        public Mediatr(Func<Type, object> serviceResolver, ConcurrentDictionary<Type, Type> handlerDetails)
        {
            _handlerDetails = handlerDetails;
            _serviceResolver = serviceResolver;
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var requestType = request.GetType();
            if (!_handlerDetails.ContainsKey(requestType))
            {
                throw new InvalidOperationException($"No handler found for {requestType.Name}");
            }

            _handlerDetails.TryGetValue(requestType, out var requestHandlerType);
            var handler = _serviceResolver(requestHandlerType);

            return await (Task<TResponse>)handler.GetType().GetMethod("HandleAsync").Invoke(handler, new []{ request });
        }
    }       
}
