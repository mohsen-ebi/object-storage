using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Simple.Object.Storage.Presentation;

public static class PresentationInjection
{
    public static IServiceCollection PresentationInject(this IServiceCollection serviceCollection)
    {
        serviceCollection.addSwagger();
        return serviceCollection;
    }

    private static IServiceCollection addSwagger(this IServiceCollection serviceCollection)
    {
        
        serviceCollection.AddSwaggerGen(
            c =>
            {
                c.AddFluentValidationRulesScoped();
                c.EnableAnnotations(true, true);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        return serviceCollection;
    }
    
     
}