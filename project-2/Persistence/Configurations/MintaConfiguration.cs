namespace Persistence.Configurations;

public class MintaConfiguration : IEntityTypeConfiguration<cMinta>
{
    public void Configure(EntityTypeBuilder<cMinta> builder)
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

        builder.HasOne(e => e.cAkkrMintavetel)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => e.AkkrMintavetel)
            .HasPrincipalKey(m => m.Id)
            .OnDelete(DeleteBehavior.Restrict); //Törlés esetén ne törölje a kapcsolódó mértékegységet

        builder.HasOne(e => e.cHUMVIfelelos)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => e.Felelos)
            .HasPrincipalKey(m => m.Felelos)
            .OnDelete(DeleteBehavior.Restrict); //Törlés esetén ne törölje a kapcsolódó mértékegységet

        builder.HasOne(e => e.cHUMVImodul)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => e.ModulKod)
            .HasPrincipalKey(m => m.ModulKod)
            .OnDelete(DeleteBehavior.Restrict); //Törlés esetén ne törölje a kapcsolódó mértékegységet

        builder.HasOne(e => e.cMintavevo)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => new { e.Mintavevo, e.MvAkkrSzam })
            .HasPrincipalKey(m => new { m.MintavevoAzonosito, m.MvAkkrSzam })
            .OnDelete(DeleteBehavior.Restrict); //Törlés esetén ne törölje a kapcsolódó mértékegységet

        builder.HasOne(e => e.cMvHely)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => e.MvhKod)
            .HasPrincipalKey(m => m.MvhKod)
            .OnDelete(DeleteBehavior.Restrict); //Törlés esetén ne törölje a kapcsolódó mértékegységet

        builder.HasOne(e => e.cMvOka)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => e.MvOk)
            .HasPrincipalKey(m => m.MvOk)
            .OnDelete(DeleteBehavior.Restrict); //Törlés esetén ne törölje a kapcsolódó mértékegységet

        builder.HasOne(e => e.cMvTipus)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => e.MvTipus)
            .HasPrincipalKey(m => m.MvTipusNev)
            .OnDelete(DeleteBehavior.Restrict); //Törlés esetén ne törölje a kapcsolódó mértékegységet

        builder.HasOne(e => e.cVizsgaloLabor)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => new { e.Labor, e.LabAkkrSzam })
            .HasPrincipalKey(m => new { m.Labor, m.LabAkkrSzam })
            .OnDelete(DeleteBehavior.Restrict); //Törlés esetén ne törölje a kapcsolódó mértékegységet




    }
}