using Accounts.Data;
using Accounts.Data.EntityFramework;
using Accounts.Patterns.Factory;
using Accounts.Patterns.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace Accounts
{
    static class Program
    {
        const string ConnectionString = @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=Accounts2021;integrated security=True;MultipleActiveResultSets=True;";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices( services );

            using (var provider = services.BuildServiceProvider())
            {
                var form = provider.GetRequiredService<MainForm>();
                Application.Run( form );
            }
        }

        private static void ConfigureServices( ServiceCollection services )
        {
            services
                .AddDbContext<DbContext,AccountsContext>( options => options.UseSqlServer( ConnectionString ) )
                .AddSingleton<IFactory,DataFactory>()
                .AddSingleton<IUnitOfWork, EntityUnitOfWork>();
            services.AddScoped<MainForm>();
        }
    }
}
