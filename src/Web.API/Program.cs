using Domain.Mappers;
using Domain.Models;
using Domain.Ports.Driven;
using Infrastructure.Adapters;
using Infrastructure.Entities;
using Infrastructure.Mapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Web.API.Models.Responses;
using Web.API.Mapper;
using Infrastructure.Mappers;
using Domain.Commands;
using Web.API.Models.Requests;
using Domain.Injection;
using Domain.Handlers;
using System.Collections.Concurrent;

namespace Web.API
{
    public class Startup
    {
        public Startup(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }
        public IConfigurationRoot Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbContextBank>(options =>
            {
                //options.UseInMemoryDatabase("Bank"));
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<DbContextBank>();

            services.AddScoped<IMapper<AccountEntity, Account>, AccountInfraMapper>();
            services.AddScoped<IMapper<AccountResponse, Account>, AccountAPIMapper>();
            services.AddScoped<IMapper<StatementEntity, Statement>, StatementInfraMapper>();
            services.AddScoped<IMapper<StatementReponse, Statement>, StatementAPIMapper>();
            services.AddScoped<IMapper<StatementRequest, StatementCommand>, StatementRequestMapper>();
            services.AddScoped<IMapper<BalanceRequest, BalanceCommand>, BalanceRequestMapper>();
            services.AddScoped<IMapper<DepositRequest, DepositCommand>, DepositRequestMapper>();
            services.AddScoped<IMapper<WithdrawRequest, WithdrawCommand>, WithdrawRequestMapper>();

            //services.AddScoped<IAccountPort, DomainAccoutAdapter>();
            services.AddScoped<IAccountPersistencePort, InfrastructureAccountAdapter>();

            services.AddRazorPages();
            services.AddAuthorization();
            services.AddControllers();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddMediator(ServiceLifetime.Scoped, typeof(Account));
            /*
            var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetRequiredService<DbContextBank>();
            DbContextBankInitializer.InitializeData(dbContext);
            */
        }
        public void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "{controller}/{action}/{id?}"
                    );
                endpoints.MapRazorPages();
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);
            var app = builder.Build();
            startup.Configure(app);
            app.Run();
        }
    }
}