namespace Persistence.Configurations;

public class AkkrMintavetelConfiguration : IEntityTypeConfiguration<cAkkrMintavetel>
{
    public void Configure(EntityTypeBuilder<cAkkrMintavetel> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.AkkrMintavetelStatusz)
            .HasMaxLength(10)
            .IsRequired();
        builder
            .Property(x => x.Leiras)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(m => m.Minta)
            .WithOne(e => e.cAkkrMintavetel)
            .HasForeignKey(e => e.AkkrMintavetel)
            .HasPrincipalKey(m => m.AkkrMintavetelStatusz)   //az idegen kulcs alapja
            .OnDelete(DeleteBehavior.Restrict); //minta törlésekor ne törölje ebbõl a táblából

    }
}