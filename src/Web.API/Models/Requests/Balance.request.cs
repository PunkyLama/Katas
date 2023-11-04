using Domain.Injection;
using Web.API.Models.Responses;

namespace Web.API.Models.Requests
{
    public class BalanceRequest : IRequest<float>
    {
        public int Id { get; set; }
    }
}
