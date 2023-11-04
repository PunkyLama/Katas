using Domain.Injection;
using Web.API.Models.Responses;

namespace Web.API.Models.Requests
{
    public class StatementRequest : IRequest<StatementReponse>
    {
        public int Id { get; set; }
        public int Element { get; set; }
    }
}
