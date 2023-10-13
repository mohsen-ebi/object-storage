using Microsoft.EntityFrameworkCore;
using Minio;
using Simple.Object.Storage.Application;
using Simple.Object.Storage.Infrastructure;
using Simple.Object.Storage.Infrastructure.Options;
using Simple.Object.Storage.Presentation;
using Simple.Object.Storage.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<MinIOConfiguration>(builder.Configuration.GetSection("MinIOConfiguration"));


builder.Services.AddEndpointsApiExplorer();


builder.Services.ApplicationInjection();
builder.Services.PresentationInject();
builder.Services.InfrastructureInjection(builder.Configuration);

// var minIoEndpoint = builder.Configuration["MinIOConfiguration:Endpoint"];
// var minIoAccessKey = builder.Configuration["MinIOConfiguration:AccessKey"];
// var minIoSecretKey = builder.Configuration["MinIOConfiguration:SecretKey"];
// var secure = false;
//builder.Services.AddMinio(accessKey: minIoAccessKey, minIoSecretKey);

// builder.Services.AddMinio((cfg) =>
//     cfg.WithEndpoint(minIoEndpoint)
//         .WithCredentials(minIoAccessKey, minIoSecretKey)
//         .WithSSL(secure)
//         .Build());


var app = builder.Build();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var scope = scopeFactory.CreateScope();

#region Initializing

await using (var pharmacyContextService = scope.ServiceProvider.GetRequiredService<StoreContext>())
{
    await pharmacyContextService.Database.MigrateAsync();
}

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();