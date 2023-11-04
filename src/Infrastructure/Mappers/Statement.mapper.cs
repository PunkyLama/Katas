using Domain.Mappers;
using Domain.Models;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappers
{
    public class StatementInfraMapper : IMapper<StatementEntity, Statement>
    {
        private readonly OperationMapper operationMapper = new OperationMapper();
        private readonly TransactionStatusMapper transactionMapper = new TransactionStatusMapper();

        public Statement MapFrom(StatementEntity input)
        {
            var operation = operationMapper.MapFrom(input.Operation);
            var transaction = transactionMapper.MapFrom(input.TransactionStatus);

            return new Statement
            {
                Id = input.Id,
                Date = input.Date,
                OperationString = input.Operation.ToString(),
                Operation = operation,
                StatementStatusString = input.TransactionStatus.ToString(),
                StatementStatus = transaction,
                Amount = input.Amount,
                OldBalance = input.OldBalance,
                NewBalance = input.NewBalance,
            };
        }

        public StatementEntity MapTo(Statement output)
        {
            var operation = operationMapper.MapTo(output.Operation);
            var transaction = transactionMapper.MapTo(output.StatementStatus);

            return new StatementEntity
            {
                Id = output.Id,
                Date = output.Date,
                Operation = operation,
                TransactionStatus = transaction,
                Amount = output.Amount,
                OldBalance = output.OldBalance,
                NewBalance = output.NewBalance,
            };
        }
    }

    public class OperationMapper : IMapper<OperationEntity, Operation>
    {
        public Operation MapFrom(OperationEntity input)
        {
            switch (input)
            {
                case OperationEntity.Deposit:
                    return Operation.Deposit;
                case OperationEntity.Withdraw:
                    return Operation.Withdraw;
                default:
                    throw new ArgumentOutOfRangeException(nameof(input));
            }
        }

        public OperationEntity MapTo(Operation output)
        {
            switch (output)
            {
                case Operation.Deposit:
                    return OperationEntity.Deposit;
                case Operation.Withdraw:
                    return OperationEntity.Withdraw;
                default:
                    throw new ArgumentOutOfRangeException(nameof(output));
            }
        }
    }

    public class TransactionStatusMapper : IMapper<StatementSatusEntity, StatementStatus>
    {
        public StatementStatus MapFrom(StatementSatusEntity input)
        {
            switch (input)
            {
                case StatementSatusEntity.Approved: return StatementStatus.Approuved;
                case StatementSatusEntity.Rejected: return StatementStatus.Rejected;
                default: throw new ArgumentOutOfRangeException(nameof(input));
            }
        }

        public StatementSatusEntity MapTo(StatementStatus output)
        {
            switch (output)
            {
                case StatementStatus.Approuved: return StatementSatusEntity.Approved;
                case StatementStatus.Rejected: return StatementSatusEntity.Rejected;
                default: throw new ArgumentOutOfRangeException(nameof(output));
            }
        }
    }
}
