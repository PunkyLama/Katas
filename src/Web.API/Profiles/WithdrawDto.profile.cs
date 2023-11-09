using AutoMapper;
using Domain.Commands;
using Web.API.Models.Dto;

namespace Web.API.Profiles
{
    public class WithdrawDtoProfile : Profile
    {
        public WithdrawDtoProfile()
        {
            CreateMap<WithdrawDto, WithdrawCommand>();
            CreateMap<WithdrawCommand, WithdrawDto>();
        }
    }
}
