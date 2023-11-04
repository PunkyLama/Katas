using Domain.Mappers;
using Domain.Models;
using Web.API.Models.Responses;

namespace Web.API.Mapper
{
    public class StatementAPIMapper : IMapper<StatementReponse, Statement>
    {
        public Statement MapFrom(StatementReponse input)
        {
            return new Statement
            {
                Id = input.Id,
                Date = input.Date,
                OperationString = input.OperationString,
                StatementStatusString = input.StatementStatusString,
                Amount = input.Amount,
                OldBalance = input.OldBalance,
                NewBalance = input.NewBalance,
            };
        }

        public StatementReponse MapTo(Statement output)
        {
            return new StatementReponse
            {
                Id = output.Id,
                Date = output.Date,
                OperationString = output.OperationString,
                StatementStatusString = output.StatementStatusString,
                Amount = output.Amount,
                OldBalance = output.OldBalance,
                NewBalance = output.NewBalance,
            };
        }

    }
}
