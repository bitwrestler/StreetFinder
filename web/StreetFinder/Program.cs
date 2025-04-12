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
            builder.Services.AddSingleton<IDataAdapter, LocalResourceDataAdpater>();
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
