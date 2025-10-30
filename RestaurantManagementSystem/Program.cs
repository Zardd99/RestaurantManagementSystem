using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagementSystem.Forms;
using RestaurantManagementSystem.Managers;
using RestaurantManagementSystem.Services;
using System;
using System.IO;
using System.Windows.Forms;

namespace RestaurantManagementSystem
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static IConfiguration Configuration { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            Application.Run(ServiceProvider.GetService<LoginForm>());
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Services
            services.AddSingleton<IApiClient, ApiClient>();

            // Managers
            services.AddTransient<UserManager>();
            services.AddTransient<OrderManager>();
            services.AddTransient<ReviewManager>();
            services.AddTransient<SupplierManager>();
            services.AddTransient<ReceiptManager>();
            services.AddTransient<CategoryManager>();
            services.AddTransient<MenuItemManager>();

            // Forms
            services.AddTransient<LoginForm>();
            services.AddTransient<MainForm>();
            services.AddTransient<UsersForm>();
            services.AddTransient<OrdersForm>();
            services.AddTransient<ReviewsForm>();
            services.AddTransient<SuppliersForm>();
            services.AddTransient<MenuForm>();

            // Dialogs
            services.AddTransient<OrderForm>();
            services.AddTransient<MenuItemForm>();
        }
    }
}