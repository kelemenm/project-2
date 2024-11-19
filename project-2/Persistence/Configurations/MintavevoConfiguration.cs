namespace Persistence.Configurations;

public class MintavevoConfiguration : IEntityTypeConfiguration<cMintavevo>
{
    public void Configure(EntityTypeBuilder<cMintavevo> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.MintavevoAzonosito)
            .HasMaxLength(10)
            .IsRequired(); 
        builder
            .Property(x => x.MvAkkrSzam)
            .HasMaxLength(15)
            .IsRequired(); 
        builder
            .Property(x => x.Nev)
            .HasMaxLength(150)
            .IsRequired(); 
        builder
            .Property(x => x.Cim)
            .HasMaxLength(150)
            .IsRequired(); 
        builder
            .Property(x => x.ErvKezdete)
            .IsRequired();
        builder
            .Property(x => x.ErvVege)
            .IsRequired();

        builder.HasMany(m => m.Minta)
            .WithOne(e => e.cMintavevo)
            .HasForeignKey(e => new { e.Mintavevo, e.MvAkkrSzam })
            .HasPrincipalKey(m => new { m.MintavevoAzonosito, m.MvAkkrSzam })   //az idegen kulcs alapja
            .OnDelete(DeleteBehavior.Restrict); //minta törlésekor ne törölje ebbõl a táblából
    }
}