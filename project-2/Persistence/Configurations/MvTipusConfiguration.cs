namespace Persistence.Configurations;

public class MvTipusConfiguration : IEntityTypeConfiguration<cMvTipus>
{
    public void Configure(EntityTypeBuilder<cMvTipus> builder)
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

        builder.HasMany(m => m.Minta)
            .WithOne(e => e.cMvTipus)
            .HasForeignKey(e => e.MvTipus)
            .HasPrincipalKey(m => m.MvTipusNev)   //az idegen kulcs alapja
            .OnDelete(DeleteBehavior.Restrict); //minta törlésekor ne törölje ebbõl a táblából
    }
}