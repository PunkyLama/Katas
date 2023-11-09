using AutoMapper;
using Domain.Commands;
using Web.API.Models.Dto;

namespace Web.API.Profiles
{
    public class DepositDtoProfile : Profile
    {
        public DepositDtoProfile()
        {
            CreateMap<DepositCommand, DepositDto>();
            CreateMap<DepositDto, DepositCommand>();
        }
    }
}
