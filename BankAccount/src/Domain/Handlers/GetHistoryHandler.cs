namespace Domain.Handlers
{
    public class GetHistoryHandler : IRequestHandler<GetHistoryQuery, ICollection<TransactionHistory>>
    {
        private readonly IAccountPort _IAccountPort;

        public GetHistoryHandler(IAccountPort IAccountPort)
        {
            _IAccountPort = IAccountPort;
        }

        public async Task<ICollection<TransactionHistory>> Handle(GetHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _IAccountPort.GetStatementByIdAsync(request.Id);
        }
    }
}
