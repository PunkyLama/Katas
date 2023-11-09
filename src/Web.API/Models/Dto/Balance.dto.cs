using Domain.Injection;

namespace Web.API.Models.Dto
{
    public class BalanceDto : IRequest<float>
    {
        public int Id { get; set; }
    }
}
