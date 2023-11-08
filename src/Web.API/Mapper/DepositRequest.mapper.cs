using Domain.Commands;
using Domain.Mappers;
using Web.API.Models.Requests;

namespace Web.API.Mapper
{
    public class DepositRequestMapper : IMapper<DepositRequest, DepositCommand>
    {
        public DepositCommand MapFrom(DepositRequest input)
        {
            return new DepositCommand
            {
                Id = input.Id,
                Amount = input.Amount
            };
        }

        public DepositRequest MapTo(DepositCommand output)
        {
            return new DepositRequest
            {
                Id = output.Id,
                Amount = output.Amount
            };
        }
    }
}
