namespace Persistence.Configurations;

public class MertekegysegConfiguration : IEntityTypeConfiguration<Mertekegyseg>
{
    public void Configure(EntityTypeBuilder<Mertekegyseg> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.Megyseg)
            .HasMaxLength(15)
            .IsRequired();
        builder
            .Property(x => x.HumviLeiras)
            .HasMaxLength(15)
            .IsRequired();
        builder
            .Property(x => x.SajatLeiras)
            .HasMaxLength(15)
            .IsRequired();
    }
}