namespace Persistence.Configurations;

public class EredmenyConfiguration : IEntityTypeConfiguration<cEredmeny>
{
    public void Configure(EntityTypeBuilder<cEredmeny> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.ParKod)
            .IsRequired();
        builder
            .Property(x => x.Megyseg)
            .IsRequired();
        builder
            .Property(x => x.Ertek)
            .HasMaxLength(25)
            .IsRequired();
        builder
            .Property(x => x.AlsoMh)
            .IsRequired(false);
        builder
            .Property(x => x.MaxRange)
            .IsRequired(false);
        builder
            .Property(x => x.ErtekHozzarendelt)
            .IsRequired(false);

        builder.HasOne(e => e.Mertekegyseg)
            .WithMany(m => m.Eredmeny)
            .HasForeignKey(e => e.Megyseg)
            .HasPrincipalKey(m => m.Id)
            .OnDelete(DeleteBehavior.Restrict); //Törlés esetén ne törölje a kapcsolódó mértékegységet

        builder.HasOne(e => e.Parameter)
            .WithMany(m => m.Eredmeny)
            .HasForeignKey(e => e.ParKod)
            .HasPrincipalKey(m => m.Id)
            .OnDelete(DeleteBehavior.Restrict); //Törlés esetén ne törölje a kapcsolódó mértékegységet
    }
}