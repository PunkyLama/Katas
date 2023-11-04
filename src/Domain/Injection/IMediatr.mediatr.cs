namespace Domain.Injection
{
    public interface IMediatr
    {
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request);
        //Task<object> SendAsync(object request);
    }
}
