using AutoMapper;
using Domain.Models;
using Web.API.Models.Responses;

namespace Web.API.Profiles
{
    public class AccountResponseProfile : Profile
    {
        public AccountResponseProfile()
        {
            CreateMap<Account, AccountResponse>();
            CreateMap<AccountResponse, Account>();
        }
    }
}
