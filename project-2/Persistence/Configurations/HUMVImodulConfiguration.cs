namespace Persistence.Configurations;

public class HUMVImodulConfiguration : IEntityTypeConfiguration<HUMVImodul>
{
    public void Configure(EntityTypeBuilder<HUMVImodul> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.ModulKod)
            .HasMaxLength(10)
            .IsRequired();
        builder
            .Property(x => x.Leiras)
            .HasMaxLength(50)
            .IsRequired();
    }
}