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
            // Register Configuration
            services.AddSingleton<IConfiguration>(Configuration);

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

            // Forms - Register MainForm with all its dependencies
            services.AddTransient<MainForm>(provider =>
            {
                return new MainForm(
                    provider.GetService<IApiClient>(),
                    provider.GetService<UserManager>(),
                    provider.GetService<OrderManager>(),
                    provider.GetService<ReviewManager>(),
                    provider.GetService<SupplierManager>(),
                    provider.GetService<ReceiptManager>(),
                    provider.GetService<CategoryManager>(),
                    provider.GetService<MenuItemManager>()
                );
            });

            services.AddTransient<LoginForm>();
            services.AddTransient<UsersForm>();
            services.AddTransient<OrdersForm>();
            services.AddTransient<ReviewsForm>();
            services.AddTransient<SuppliersForm>();
            services.AddTransient<ReceiptsForm>();
            services.AddTransient<CategoriesForm>();
            services.AddTransient<MenuForm>();

            // Dialogs
            services.AddTransient<OrderForm>();
            services.AddTransient<MenuItemForm>();
            services.AddTransient<StatusUpdateForm>();
            services.AddTransient<OrderStatsForm>();
            services.AddTransient<OrderItemForm>();
        }
    }
}