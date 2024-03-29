using Domain.Models;
using Domain.Ports.Driven;
using Infrastructure.Adapters;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Infrastructure.Profiles;
using Web.API.Profiles;
using Infrastructure.Respositories;

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
            services.AddAutoMapper(typeof(AccountEntityProfile), typeof(AccountResponseProfile));

            services.AddScoped<IAccountPersistencePort, SQLAccountAdapter>();

            //services.AddRazorPages();
            services.AddAuthorization();
            services.AddControllers();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddMediator(ServiceLifetime.Scoped, typeof(Account));

            /*
             * Angular config
             */

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });            

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
                //endpoints.MapRazorPages();
            });
        }
    }
}