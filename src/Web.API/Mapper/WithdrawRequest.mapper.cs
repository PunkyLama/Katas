using Domain.Commands;
using Domain.Mappers;
using Web.API.Models.Requests;

namespace Web.API.Mapper
{
    public class WithdrawRequestMapper : IMapper<WithdrawRequest, WithdrawCommand>
    {
        public WithdrawCommand MapFrom(WithdrawRequest input)
        {
            return new WithdrawCommand
            {
                Id = input.Id,
                Amount = input.Amount
            };
        }

        public WithdrawRequest MapTo(WithdrawCommand output)
        {
            return new WithdrawRequest
            {
                Id = output.Id,
                Amount = output.Amount
            };
        }
    }
}
