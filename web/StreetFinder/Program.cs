using ers_config;

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
            app.MapRazorPages();

#if DEBUG
            DebugConfig.ProcessDotEnvFile(DebugConfig.FindAzureConfigInParents(".env"));
#endif
            app.Run();
        }
    }
}
