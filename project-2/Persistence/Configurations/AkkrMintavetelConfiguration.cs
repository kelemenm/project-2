namespace Persistence.Configurations;

public class AkkrMintavetelConfiguration : IEntityTypeConfiguration<AkkrMintavetel>
{
    public void Configure(EntityTypeBuilder<AkkrMintavetel> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.AkkrMintavetelStatusz)
            .HasMaxLength(10)
            .IsRequired();
        builder
            .Property(x => x.Leiras)
            .HasMaxLength(50)
            .IsRequired();
    }
}