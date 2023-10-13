using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Simple.Object.Storage.Infrastructure.Options;

namespace Simple.Object.Storage.Infrastructure;

public static class InfrastructureInjectionLayer
{
    public static IServiceCollection InfrastructureInjection(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddSqlServerDatabase(configuration: configuration);
        serviceCollection.AddRepositories();
        serviceCollection.RegisterMinIo(configuration);
        return serviceCollection;
    }

    private static IServiceCollection AddSqlServerDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:SqlConnection"];


        services.AddDbContext<StoreContext>(
            options => options.UseSqlServer(connectionString,
                x => x.MigrationsAssembly("Simple.Store.Infrastructure")
            )
        );

        return services;
    }

    private static void AddRepositories(this IServiceCollection serviceCollection)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(asm => asm.GetModules())
            .Where(h => h.Name.Contains("Infrastructure"))
            .ToList();
        assemblies.ForEach(asm =>
        {
            var rd = asm.GetTypes().ToList();
            var servicesToInject = asm.GetTypes().Where(h => h.IsClass && h.Name.Contains("Repository")).ToList();
            foreach (var svc in servicesToInject)
            {
                var Isvc = svc.GetInterfaces().FirstOrDefault(h => h.Name.Contains(svc.Name));
                if (Isvc != null)
                    serviceCollection.AddTransient(Isvc, svc);
            }
        });
    }

    private static IServiceCollection RegisterMinIo(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var minIoEndpoint = configuration["MinIOConfiguration:Endpoint"];
        var minIoAccessKey = configuration["MinIOConfiguration:AccessKey"];
        var minIoSecretKey = configuration["MinIOConfiguration:SecretKey"];

        //serviceCollection.AddMinio(accessKey: minIoAccessKey, minIoSecretKey);

        serviceCollection.AddMinio(configureClient =>
            configureClient.WithEndpoint(new Uri(minIoEndpoint))
                .WithCredentials(minIoAccessKey, minIoSecretKey)
                .WithSSL(false)
                .Build());
        return serviceCollection;
    }
}