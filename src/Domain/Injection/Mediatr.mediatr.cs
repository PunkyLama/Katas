using System.Collections.Concurrent;

namespace Domain.Injection
{
    public class Mediatr : IMediatr
    {
        private readonly Func<Type, object> _serviceResolver;
        private readonly ConcurrentDictionary<Type, Type> _handlerDetails;

        public Mediatr(ConcurrentDictionary<Type, Type> handlerDetails, Func<Type, object> serviceResolver = null)
        {
            _serviceResolver = serviceResolver;
            _handlerDetails = handlerDetails;
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
            var handleAsyncMethod = handler.GetType().GetMethod("HandleAsync");

            return await (Task<TResponse>)handleAsyncMethod.Invoke(handler, new[] { request });

        }
    }
}
