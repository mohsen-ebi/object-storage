using System.Reflection;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Simple.Object.Storage.Application.Utils;

namespace Simple.Object.Storage.Application;

public static class ApplicationInjectionLayer
{
    public static void ApplicationInjection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluentValidators();
        serviceCollection.AddMasstransit();
    }

    private static void AddFluentValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
    }

    private static void AddMasstransit(this IServiceCollection serviceCollection)
    {
        const string domainCommandName = nameof(IContract);
        var executingAssembly = Assembly.GetExecutingAssembly();
        var requestClients = executingAssembly.GetTypes()
            .Where(t => t.GetInterface(domainCommandName) is not null &&
                        t.Name.Contains(domainCommandName) is false
            )
            .Distinct()
            .ToList();
        serviceCollection.AddMediator(cfg =>
        {
            cfg.AddConsumers(executingAssembly);
            requestClients.ForEach(contract => cfg.AddRequestClient(contract));
        });
    }
}