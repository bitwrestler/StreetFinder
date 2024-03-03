using ers_config;
using Microsoft.Extensions.Azure;
using StreetFinder.Code;

namespace StreetFinder
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            DebugConfig.ProcessDotEnvFile(DebugConfig.FindAzureConfigInParents(".env"));
#endif
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSingleton<IDataAdapter,AzureDataAdapter>();
            builder.Services.AddHttpClient<IAzureMapSericeClient, AzureMapServiceClient>();
            var app = builder.Build();

            //warm up the data service
            app.Lifetime.ApplicationStarted.Register(() => app.Services.GetService(typeof(IDataAdapter)));

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();
            app.MapRazorPages();
            app.Run();
        }
    }
}
