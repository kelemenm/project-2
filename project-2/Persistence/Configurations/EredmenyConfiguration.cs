namespace Persistence.Configurations;

public class EredmenyConfiguration : IEntityTypeConfiguration<Eredmeny>
{
    public void Configure(EntityTypeBuilder<Eredmeny> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.Parkod)
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
            .Property(x => x.Alsomh)
            .IsRequired(false);
        builder
            .Property(x => x.MaxRange)
            .IsRequired(false);
        builder
            .Property(x => x.ErtekHozzarendelt)
            .IsRequired(false);

    }
}