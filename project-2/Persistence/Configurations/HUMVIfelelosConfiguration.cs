namespace Persistence.Configurations;

public class HUMVIfelelosConfiguration : IEntityTypeConfiguration<cHUMVIfelelos>
{
    public void Configure(EntityTypeBuilder<cHUMVIfelelos> builder)
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

        builder.HasMany(m => m.Minta)
            .WithOne(e => e.cHUMVIfelelos)
            .HasForeignKey(e => e.Felelos)
            .HasPrincipalKey(m => m.Id)   //az idegen kulcs alapja
            .OnDelete(DeleteBehavior.Restrict); //minta törlésekor ne törölje ebbõl a táblából

        builder.HasIndex(m => m.Felelos).IsUnique(); // Egyediség biztosítása
    }
}