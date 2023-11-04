using Domain.Mappers;
using Domain.Models;
using Web.API.Models.Responses;

namespace Web.API.Mapper
{
    public class AccountAPIMapper : IMapper<AccountResponse, Account>
    {
        public Account MapFrom(AccountResponse input)
        {
            return new Account
            {
                Balance = input.Balance,
                Id = input.Id
            };
        }

        public AccountResponse MapTo(Account output)
        {
            return new AccountResponse
            {
                Balance = output.Balance,
                Id = output.Id
            };
        }
    }
}
