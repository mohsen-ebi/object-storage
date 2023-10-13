using Microsoft.Extensions.Configuration;

namespace Simple.Object.Storage.Infrastructure;

public class StoreContext : DbContext
{
    private readonly IConfiguration _configuration;

    public StoreContext(DbContextOptions<StoreContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}