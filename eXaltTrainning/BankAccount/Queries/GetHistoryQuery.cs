using KataBankAccount.Models;
using MediatR;

namespace KataBankAccount.Queries
{
    public class GetHistoryQuery : IRequest<string>
    {
        public int Id { get; set; }
    }
}
