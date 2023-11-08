using Domain.Mappers;
using Domain.Queries;
using Web.API.Models.Requests;

namespace Web.API.Mapper
{
    public class BalanceRequestMapper : IMapper<BalanceRequest, BalanceQuery>
    {
        public BalanceQuery MapFrom(BalanceRequest input)
        {
            return new BalanceQuery
            {
                Id = input.Id
            };
        }

        public BalanceRequest MapTo(BalanceQuery output)
        {
            return new BalanceRequest
            {
                Id = output.Id
            };
        }
    }
}
