namespace Persistence.Configurations;

public class MintavevoConfiguration : IEntityTypeConfiguration<Mintavevo>
{
    public void Configure(EntityTypeBuilder<Mintavevo> builder)
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
            .IsRequired(false);
        builder
            .Property(x => x.ErvVege)
            .IsRequired(false);
    }
}