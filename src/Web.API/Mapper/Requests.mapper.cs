using Domain.Commands;
using Domain.Mappers;
using Web.API.Models.Requests;

namespace Web.API.Mapper
{
    public class BalanceRequestMapper : IMapper<BalanceRequest, BalanceCommand>
    {
        public BalanceCommand MapFrom(BalanceRequest input)
        {
            return new BalanceCommand
            {
                Id = input.Id
            };
        }

        public BalanceRequest MapTo(BalanceCommand output)
        {
            return new BalanceRequest
            {
                Id = output.Id
            };
        }
    }
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
    public class StatementRequestMapper : IMapper<StatementRequest, StatementCommand>
    {
        public StatementCommand MapFrom(StatementRequest input)
        {
            return new StatementCommand
            {
                Id = input.Id,
                Element = input.Element
            };
        }

        public StatementRequest MapTo(StatementCommand output)
        {
            return new StatementRequest
            {
                Id = output.Id,
                Element = output.Element
            };
        }
    }
}
