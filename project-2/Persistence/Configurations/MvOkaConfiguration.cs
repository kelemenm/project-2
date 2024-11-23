namespace Persistence.Configurations;

public class MvOkaConfiguration : IEntityTypeConfiguration<cMvOka>
{
    public void Configure(EntityTypeBuilder<cMvOka> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.MvOk)
            .HasMaxLength(10)
            .IsRequired(); 
        builder
            .Property(x => x.Leiras)
            .HasMaxLength(15)
            .IsRequired();

        builder.HasMany(m => m.Minta)
            .WithOne(e => e.cMvOka)
            .HasForeignKey(e => e.MvOk)
            .HasPrincipalKey(m => m.Id)   //az idegen kulcs alapja
            .OnDelete(DeleteBehavior.Restrict); //minta t�rl�sekor ne t�r�lje ebb�l a t�bl�b�l

        builder.HasIndex(m => m.MvOk).IsUnique(); // Egyedis�g biztos�t�sa
    }
}