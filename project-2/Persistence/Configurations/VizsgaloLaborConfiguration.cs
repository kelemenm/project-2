namespace Persistence.Configurations;

public class VizsgaloLaborConfiguration : IEntityTypeConfiguration<VizsgaloLabor>
{
    public void Configure(EntityTypeBuilder<VizsgaloLabor> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.Labor)
            .HasMaxLength(10)
            .IsRequired(); 
        builder
            .Property(x => x.Labakkrszam)
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