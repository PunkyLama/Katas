using Domain.Mappers;
using Domain.Models;
using Infrastructure.Entities;

namespace Infrastructure.Mapper
{
    public class AccountMapper : IMapper<AccountEntity, Account>
    {
        public Account MapFrom(AccountEntity input)
        {
            return new Account
            {
                Balance = input.Balance,
                Id = input.Id,
                TransactionHistories = (ICollection<TransactionHistory>)input.TransactionHistories
            };
        }

        public AccountEntity MapTo(Account output)
        {
            return new AccountEntity
            {
                Balance = output.Balance,
                Id = output.Id,
                TransactionHistories = (ICollection<TransactionHistoryEntity>)output.TransactionHistories
            };
        }
    }
}
