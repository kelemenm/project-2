namespace Persistence.Configurations;

public class MertekegysegConfiguration : IEntityTypeConfiguration<cMertekegyseg>
{
    public void Configure(EntityTypeBuilder<cMertekegyseg> builder)
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

        builder.HasMany(m => m.Eredmeny)
            .WithOne(e => e.Mertekegyseg)
            .HasForeignKey(e => e.Megyseg)
            .HasPrincipalKey(m => m.Id)   //Megyseg az idegen kulcs alapja
            .OnDelete(DeleteBehavior.Restrict); //Eredm�nyek t�rl�sekor ne t�r�lje a m�rt�kegys�get

        builder.HasIndex(m => m.Megyseg).IsUnique(); // Egyedis�g biztos�t�sa
    }
}