namespace Persistence.Configurations;

public class MvHelyConfiguration : IEntityTypeConfiguration<MvHely>
{
    public void Configure(EntityTypeBuilder<MvHely> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.MvhKod)
            .HasMaxLength(75)
            .IsRequired(); 
        builder
            .Property(x => x.NevSajat)
            .HasMaxLength(75)
            .IsRequired(); 
        builder
            .Property(x => x.NevTeljes)
            .HasMaxLength(75)
            .IsRequired(); 
        builder
            .Property(x => x.VizBazis)
            .HasMaxLength(75)
            .IsRequired();
        builder
            .Property(x => x.NevRovid)
            .HasMaxLength(75)
            .IsRequired(); 
        builder
            .Property(x => x.Telepules)
            .HasMaxLength(75)
            .IsRequired(); 
        builder
            .Property(x => x.Tipus)
            .HasMaxLength(75)
            .IsRequired(); 
        builder
            .Property(x => x.HumviRegiNev)
            .HasMaxLength(75)
            .IsRequired(false); 
        builder
            .Property(x => x.GPS_E_X)
            .IsRequired(false); 
        builder
            .Property(x => x.GPS_N_Y)
            .IsRequired(false); 
    }
}