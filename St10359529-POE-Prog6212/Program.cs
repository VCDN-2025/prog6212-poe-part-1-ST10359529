namespace St10359529_POE_Prog6212
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(); // Enable session support

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession(); // Use session middleware
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

// References
// ----------
// GetBootstrap (2023) Bootstrap v5.1.3 documentation. Available at: https://getbootstrap.com/docs/5.1/getting-started/introduction/ (Accessed: 22 October 2025).
// Microsoft (2023) ASP.NET Core documentation. Available at: https://docs.microsoft.com/en-us/aspnet/core (Accessed: 22 October 2025).
// Microsoft (2023) MSTest framework documentation. Available at: https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest (Accessed: 22 October 2025).
// Stack Overflow (2023) Various community answers on ASP.NET Core and C#. Available at: https://stackoverflow.com/ (Accessed: 22 October 2025).
// --------