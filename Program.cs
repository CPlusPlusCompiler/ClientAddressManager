using ClientAddressManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
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
                        }
                        catch(NullReferenceException e)
                        {
                            Console.WriteLine(e.StackTrace); 
                        }
                    });
                }).Build();

            using (var serviceScope = host.Services.CreateScope()) 
            {
                var services = serviceScope.ServiceProvider;
                var databaseContext = services.GetRequiredService<DatabaseContext>();
                var clientsRepository = new ClientsRepository(databaseContext);
                var clientsViewModel = new ClientsViewModel(clientsRepository);

                Application.Run(new MainForm(clientsViewModel));
            }
        }
    }
}
