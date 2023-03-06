using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoWebAPI;
using MongoWebAPI.Repositories;

static void Main(string[] args)
{
    var host = new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        })
        .ConfigureServices((hostingContext, services) =>
        {
            services.Configure<RepoOptions>(hostingContext.Configuration.GetSection(nameof(RepoOptions)));
            services.AddSingleton<IUserRepository, UserRepository>();
        })
        .UseStartup<Startup>()
        .Build();

    host.Run();
}
