using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simple.Object.Storage.Infrastructure.Entities.Store;

public class StoreEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Store>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Store> builder)
    {
        builder.ToTable("Store", "Store");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Tag).IsRequired();
        builder.Property(c => c.FileType).IsRequired();
    }
}