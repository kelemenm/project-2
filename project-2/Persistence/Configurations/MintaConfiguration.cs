namespace Persistence.Configurations;

public class MintaConfiguration : IEntityTypeConfiguration<Minta>
{
    public void Configure(EntityTypeBuilder<Minta> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.LaborMintaKod)
            .HasMaxLength(25)
            .IsRequired();
        builder
            .Property(x => x.ModulKod)
            .HasMaxLength(10)
            .IsRequired();
        builder
            .Property(x => x.Felelos)
            .HasMaxLength(10)
            .IsRequired();
        builder
            .Property(x => x.MvTipus)
            .HasMaxLength(10)
            .IsRequired();
        builder
            .Property(x => x.Labor)
            .HasMaxLength(10)
            .IsRequired();
        builder
            .Property(x => x.LabAkkrSzam)
            .HasMaxLength(15)
            .IsRequired(false);
        builder
            .Property(x => x.MvOk)
            .HasMaxLength(10)
            .IsRequired();
        builder
            .Property(x => x.MvOkaEgyeb)
            .HasMaxLength(255)
            .IsRequired(false);
        builder
            .Property(x => x.MvhKod)
            .HasMaxLength(25)
            .IsRequired();
        builder
            .Property(x => x.MvHely)
            .HasMaxLength(255)
            .IsRequired(false);
        builder
            .Property(x => x.AkkrMintavetel)
            .HasMaxLength(10)
            .IsRequired(false);
        builder
            .Property(x => x.Mintavevo)
            .HasMaxLength(10)
            .IsRequired(false);
        builder
            .Property(x => x.MvAkkrSzam)
            .HasMaxLength(15)
            .IsRequired(false);
        builder
            .Property(x => x.MvDatum)
            .IsRequired();
        builder
            .Property(x => x.MintaAtvetel)
            .IsRequired();
        builder
            .Property(x => x.VizsgalatKezdete)
            .IsRequired();
        builder
            .Property(x => x.VizsgalatVege)
            .IsRequired();
        builder
            .Property(x => x.HUMVIexport)
            .IsRequired();
    }
}