using Data;
using Domain.Ports.Driving;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddDbContext<DbContextBank>(options =>
                options.UseInMemoryDatabase(databaseName: "BankAccount"));
            services.AddScoped<IAccountPort, AccountAdapter>();
            services.AddAuthorization();
            services.AddControllers();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }
        public void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
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