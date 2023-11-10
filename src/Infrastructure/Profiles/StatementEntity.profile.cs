using AutoMapper;
using Domain.Models;
using Infrastructure.Entities;

namespace Infrastructure.Profiles
{
    public class StatementEntityProfile : Profile
    {
        public StatementEntityProfile()
        {
            CreateMap<Statement, StatementEntity>()
            .ForMember(dest => dest.TransactionStatus, opt => opt.MapFrom(src => src.StatementStatus))
            .ForMember(dest => dest.Operation, opt => opt.MapFrom(src => src.Operation));

            CreateMap<StatementEntity, Statement>()
                .ForMember(dest => dest.StatementStatus, opt => opt.MapFrom(src => src.TransactionStatus))
                .ForMember(dest => dest.Operation, opt => opt.MapFrom(src => src.Operation))
                .ForMember(dest => dest.OperationString, opt => opt.MapFrom(src => src.Operation.ToString()))
                .ForMember(dest => dest.StatementStatusString, opt => opt.MapFrom(src => src.TransactionStatus.ToString()));
        }
    }
}
