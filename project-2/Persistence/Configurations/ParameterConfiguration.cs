namespace Persistence.Configurations;

public class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
{
    public void Configure(EntityTypeBuilder<Parameter> builder)
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
            .HasForeignKey(equals => equals.ParKod)
            .HasPrincipalKey(m => m.ParKod)   //Parkod az idegen kulcs alapja
            .OnDelete(DeleteBehavior.Restrict); //Eredmények törlésekor ne törölje a paramétert

    }
}