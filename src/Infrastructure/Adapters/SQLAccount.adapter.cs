using AutoMapper;
using Infrastructure.Respositories;
using Infrastructure.Context;

namespace Infrastructure.Adapters
{
    public class SQLAccountAdapter : BaseAccountPersistencePort<DbContextBank>
    {
        public SQLAccountAdapter(DbContextBank dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}