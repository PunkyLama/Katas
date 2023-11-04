using Domain.Injection;
using Web.API.Models.Responses;

namespace Web.API.Models.Requests
{
    public class WithdrawRequest : IRequest<AccountResponse>
    {
        public int Id { get; set; }
        public float Amount { get; set; }
    }
}
