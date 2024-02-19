using ers_config;
using StreetFinder.Code;

namespace StreetFinder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();
            app.MapRazorPages();

#if DEBUG
            DebugConfig.ProcessDotEnvFile(DebugConfig.FindAzureConfigInParents(".env"));
#endif
            _ = AzureDataAdapter.Instance;
            app.Run();
        }
    }
}
