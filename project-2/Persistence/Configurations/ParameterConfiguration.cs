namespace Persistence.Configurations;

public class ParameterConfiguration : IEntityTypeConfiguration<cParameter>
{
    public void Configure(EntityTypeBuilder<cParameter> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.ParKod)
            .HasMaxLength(25)
            .IsRequired(); 
        builder
            .Property(x => x.HumviLeiras)
            .HasMaxLength(75)
            .IsRequired(); 
        builder
            .Property(x => x.SajatLeiras)
            .HasMaxLength(75)
            .IsRequired(); 
        builder
            .Property(x => x.ParamErtek)
            .HasMaxLength(75)
            .IsRequired(false); 
        builder
            .Property(x => x.ParamTip)
            .HasMaxLength(25)
            .IsRequired(false);

        builder.HasMany(m => m.Eredmeny)
            .WithOne(e => e.Parameter)
            .HasForeignKey(e => e.ParKod)
            .HasPrincipalKey(m => m.Id)   //Parkod az idegen kulcs alapja
            .OnDelete(DeleteBehavior.Restrict); //Eredm�nyek t�rl�sekor ne t�r�lje a param�tert

        builder.HasIndex(m => m.ParKod).IsUnique(); // Egyedis�g biztos�t�sa

    }
}