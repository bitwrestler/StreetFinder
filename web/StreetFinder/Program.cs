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
            builder.Services.AddSingleton<AzureDataAdapter>();
            var app = builder.Build();
            
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();
            app.MapRazorPages();
            app.Run();
        }
    }
}
