using Domain.Mappers;
using Domain.Queries;
using Web.API.Models.Requests;

namespace Web.API.Mapper
{
    public class StatementRequestMapper : IMapper<StatementRequest, StatementQuery>
    {
        public StatementQuery MapFrom(StatementRequest input)
        {
            return new StatementQuery
            {
                Id = input.Id,
                Element = input.Element
            };
        }

        public StatementRequest MapTo(StatementQuery output)
        {
            return new StatementRequest
            {
                Id = output.Id,
                Element = output.Element
            };
        }
    }
}
