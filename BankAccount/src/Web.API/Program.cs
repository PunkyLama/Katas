using Domain.Adapters;
using Domain.Mappers;
using Domain.Models;
using Domain.Ports.Driven;
using Domain.Ports.Driving;
using Infrastructure.Adapters;
using Infrastructure.Entities;
using Infrastructure.Mapper;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

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
            services.AddRazorPages();
            services.AddDbContext<DbContextBank>(options =>
            {
                //options.UseInMemoryDatabase("Bank"));
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });           

            services.AddScoped<DbContextBank>();

            services.AddScoped<IMapper<AccountEntity, Account>, AccountMapper>();

            services.AddScoped<IAccountPort, DomainAccoutAdapter>();
            services.AddScoped<IAccountPersistencePort, InfrastructureAccountAdapter>();

            services.AddAuthorization();
            services.AddControllers();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

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