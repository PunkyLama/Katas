using Domain.Injection;
using Web.API.Models.Responses;

namespace Web.API.Models.Dto
{
    public class WithdrawDto : IRequest<AccountResponse>
    {
        public int Id { get; set; }
        public float Amount { get; set; }
    }
}
