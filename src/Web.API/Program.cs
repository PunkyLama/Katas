namespace Web.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);
            var app = builder.Build();
            //Add angular 
            app.UseCors("AllowAllOrigins");
            startup.Configure(app);
            app.Run();
        }
    }
}
