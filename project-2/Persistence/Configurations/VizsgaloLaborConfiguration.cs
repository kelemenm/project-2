namespace Persistence.Configurations;

public class VizsgaloLaborConfiguration : IEntityTypeConfiguration<cVizsgaloLabor>
{
    public void Configure(EntityTypeBuilder<cVizsgaloLabor> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.Labor)
            .HasMaxLength(10)
            .IsRequired(); 
        builder
            .Property(x => x.LabAkkrSzam)
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
            .WithOne(e => e.cVizsgaloLabor)
            .HasForeignKey(e => e.Labor)
            .HasPrincipalKey(m => m.Id)   //az idegen kulcs alapja
            .OnDelete(DeleteBehavior.Restrict); //minta törlésekor ne törölje ebbõl a táblából

        builder.HasIndex(m => new { m.Labor, m.LabAkkrSzam }).IsUnique(); // Egyediség biztosítása
    }
}