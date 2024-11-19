namespace Persistence.Configurations;

public class HUMVImodulConfiguration : IEntityTypeConfiguration<cHUMVImodul>
{
    public void Configure(EntityTypeBuilder<cHUMVImodul> builder)
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

        builder.HasMany(m => m.Minta)
            .WithOne(e => e.cHUMVImodul)
            .HasForeignKey(e => e.ModulKod)
            .HasPrincipalKey(m => m.ModulKod)   //az idegen kulcs alapja
            .OnDelete(DeleteBehavior.Restrict); //minta törlésekor ne törölje ebbõl a táblából
    }
}