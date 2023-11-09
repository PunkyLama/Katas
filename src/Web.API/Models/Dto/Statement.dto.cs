using Domain.Injection;
using Web.API.Models.Responses;

namespace Web.API.Models.Dto
{
    public class StatementDto : IRequest<StatementReponse>
    {
        public int Id { get; set; }
        public int Element { get; set; }
    }
}
