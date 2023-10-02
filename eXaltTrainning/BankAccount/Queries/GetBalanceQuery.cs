using MediatR;

namespace KataBankAccount.Queries
{
    public class GetBalanceQuery : IRequest<float>
    {
        public int Id { get; set; }
    }
}
