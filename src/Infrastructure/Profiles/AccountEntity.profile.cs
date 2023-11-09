using Domain.Models;
using Infrastructure.Entities;
using AutoMapper;

namespace Infrastructure.Profiles
{
    public class AccountEntityProfile : Profile
    {
        public AccountEntityProfile()
        {
            CreateMap<Account, AccountEntity>();
            CreateMap<AccountEntity, Account>();
        }
    }
}
