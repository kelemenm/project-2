namespace Persistence.Configurations;

public class EredmenyConfiguration : IEntityTypeConfiguration<Eredmeny>
{
    public void Configure(EntityTypeBuilder<Eredmeny> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.ParKod)
            .HasMaxLength(25)
            .IsRequired();
        builder
            .Property(x => x.Megyseg)
            .HasMaxLength(15)
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
            .HasPrincipalKey(m => m.Megyseg)
            .OnDelete(DeleteBehavior.Restrict); //Törlés esetén ne törölje a kapcsolódó mértékegységet

        builder.HasOne(e => e.Parameter)
            .WithMany(m => m.Eredmeny)
            .HasForeignKey(e => e.ParKod)
            .HasPrincipalKey(m => m.ParKod)
            .OnDelete(DeleteBehavior.Restrict); //Törlés esetén ne törölje a kapcsolódó mértékegységet
    }
}