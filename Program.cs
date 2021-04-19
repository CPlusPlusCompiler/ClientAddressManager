using ClientAddressManager.Models;
using ClientAddressManager.Models.DataSources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace ClientAddressManager
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // configuring database and dependency injection
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<DatabaseContext>(options =>
                    {
                        try
                        {
                            var connectionStr = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
                            options.UseSqlServer(connectionStr);
                            options.LogTo(Console.WriteLine);
                        }
                        catch(NullReferenceException e)
                        {
                            Console.WriteLine(e.StackTrace); 
                        }
                    });

                    services.AddScoped<DatabaseContext>();
                    services.AddScoped<IClientsRepository, ClientsRepository>();
                    services.AddScoped<IPostCodeService, PostCodeService>();
                }).Build();

            using (var serviceScope = host.Services.CreateScope()) 
            {
                var services = serviceScope.ServiceProvider;
                //var databaseContext = services.GetRequiredService<DatabaseContext>();
                var clientsRepository = services.GetRequiredService<IClientsRepository>();//new ClientsRepository(databaseContext);
                var postService = services.GetRequiredService<IPostCodeService>();

                var clientsViewModel = new ClientsViewModel(clientsRepository, postService);

                Application.Run(new MainForm(clientsViewModel));
            }
        }
    }
}
