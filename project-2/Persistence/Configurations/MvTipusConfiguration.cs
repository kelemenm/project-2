namespace Persistence.Configurations;

public class MvTipusConfiguration : IEntityTypeConfiguration<MvTipus>
{
    public void Configure(EntityTypeBuilder<MvTipus> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.MvTipusNev)
            .HasMaxLength(10)
            .IsRequired(); 
        builder
            .Property(x => x.Leiras)
            .HasMaxLength(50)
            .IsRequired(); 
    }
}