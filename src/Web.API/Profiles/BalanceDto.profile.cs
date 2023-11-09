using AutoMapper;
using Domain.Queries;
using Web.API.Models.Dto;

namespace Web.API.Profiles
{
    public class BalanceDtoProfile : Profile
    {
        public BalanceDtoProfile()
        {
            CreateMap<BalanceQuery, BalanceDto>();
            CreateMap<BalanceDto, BalanceQuery>();
        }
    }
}
