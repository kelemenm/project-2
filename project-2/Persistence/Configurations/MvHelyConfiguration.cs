namespace Persistence.Configurations;

public class MvHelyConfiguration : IEntityTypeConfiguration<cMvHely>
{
    public void Configure(EntityTypeBuilder<cMvHely> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.MvhKod)
            .HasMaxLength(25)
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

        builder.HasMany(m => m.Minta)
            .WithOne(e => e.cMvHely)
            .HasForeignKey(e => e.MvhKod)
            .HasPrincipalKey(m => m.Id)   //az idegen kulcs alapja
            .OnDelete(DeleteBehavior.Restrict); //minta törlésekor ne törölje ebbõl a táblából

        builder.HasIndex(m => m.MvhKod).IsUnique(); // Egyediség biztosítása
    }
}