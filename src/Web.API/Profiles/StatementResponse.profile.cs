using AutoMapper;
using Domain.Models;
using Web.API.Models.Responses;

namespace Infrastructure.Profiles
{
    public class StatementResponseProfile : Profile
    {
        public StatementResponseProfile()
        {
            CreateMap<Statement, StatementReponse>();
            CreateMap<StatementReponse, Statement>();
        }
    }
}