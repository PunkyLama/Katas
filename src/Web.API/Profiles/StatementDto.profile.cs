using AutoMapper;
using Domain.Queries;
using Web.API.Models.Dto;

namespace Web.API.Profiles
{
    public class StatementDtoProfile : Profile
    {
        public StatementDtoProfile()
        {
            CreateMap<StatementQuery, StatementDto>();
            CreateMap<StatementDto, StatementQuery>();
        }
    }
}
