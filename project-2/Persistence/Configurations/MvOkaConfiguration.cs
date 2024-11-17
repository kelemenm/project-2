namespace Persistence.Configurations;

public class MvOkaConfiguration : IEntityTypeConfiguration<MvOka>
{
    public void Configure(EntityTypeBuilder<MvOka> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.Oka)
            .HasMaxLength(10)
            .IsRequired(); 
        builder
            .Property(x => x.Leiras)
            .HasMaxLength(15)
            .IsRequired(); 
    }
}