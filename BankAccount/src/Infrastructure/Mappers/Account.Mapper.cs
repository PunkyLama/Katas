using Domain.Mappers;
using Domain.Models;
using Infrastructure.Entities;

namespace Infrastructure.Mapper
{
    public class AccountMapper : IMapper<AccountEntity, Account>
    {
        private TransactionMapper transactionMapper = new TransactionMapper();
        public Account MapFrom(AccountEntity input)
        {
            List<TransactionHistory> transactionHistories = input.TransactionHistories
            .Select(transaction => transactionMapper.MapFrom(transaction))
            .ToList();

            return new Account
            {
                Balance = input.Balance,
                Id = input.Id,
                TransactionHistories = transactionHistories
            };
        }

        public AccountEntity MapTo(Account output)
        {
            List<TransactionHistoryEntity> transactionHistories = output.TransactionHistories
            .Select(transaction => transactionMapper.MapTo(transaction))
            .ToList();

            return new AccountEntity
            {
                Balance = output.Balance,
                Id = output.Id,
                TransactionHistories = transactionHistories
            };
        }
    }

    public class TransactionMapper : IMapper<TransactionHistoryEntity, TransactionHistory>
    {
        private OperationMapper operationMapper = new OperationMapper();
        private TransactionHistoryMapper transactionMapper = new TransactionHistoryMapper();

        public TransactionHistory MapFrom(TransactionHistoryEntity input)
        {
            return new TransactionHistory
            {
                AccountId = input.AccountId,
                Id = input.Id,
                Date = input.Date,
                OperationString = input.Operation.ToString(),
                TransactionStatusString = input.TransactionStatus.ToString()
            };
        }

        public TransactionHistoryEntity MapTo(TransactionHistory output)
        {
            var operation = operationMapper.MapTo(output.Operation);
            var transaction = transactionMapper.MapTo(output.TransactionStatus);

            return new TransactionHistoryEntity
            {
                AccountId = output.AccountId,
                Id = output.Id,
                Date = output.Date,
                Operation = operation,
                TransactionStatus = transaction
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

    public class TransactionHistoryMapper : IMapper<TransactionStatusEntity, TransactionStatus>
    {
        public TransactionStatus MapFrom(TransactionStatusEntity input)
        {
            switch (input)
            {
                case TransactionStatusEntity.Approved: return TransactionStatus.Approuved;
                case TransactionStatusEntity.Rejected: return TransactionStatus.Rejected;
                default: throw new ArgumentOutOfRangeException(nameof(input));
            }
        }

        public TransactionStatusEntity MapTo(TransactionStatus output)
        {
            switch (output)
            {
                case TransactionStatus.Approuved: return TransactionStatusEntity.Approved;
                case TransactionStatus.Rejected: return TransactionStatusEntity.Rejected;
                default: throw new ArgumentOutOfRangeException(nameof(output));
            }
        }
    }
}
