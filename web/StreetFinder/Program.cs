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
            app.Run();
        }
    }
}
