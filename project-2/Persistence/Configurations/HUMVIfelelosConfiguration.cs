namespace Persistence.Configurations;

public class HUMVIfelelosConfiguration : IEntityTypeConfiguration<HUMVIfelelos>
{
    public void Configure(EntityTypeBuilder<HUMVIfelelos> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.Felelos)
            .HasMaxLength(10)
            .IsRequired();
        builder
            .Property(x => x.Nev)
            .HasMaxLength(150)
            .IsRequired();
        builder
            .Property(x => x.Cim)
            .HasMaxLength(150)
            .IsRequired();
    }
}