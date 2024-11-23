namespace Persistence.Configurations;

public class MintaConfiguration : IEntityTypeConfiguration<cMinta>
{
    public void Configure(EntityTypeBuilder<cMinta> builder)
    {
        builder.HasIndex(x => x.Id);
        builder
            .Property(x => x.LaborMintaKod)
            .HasMaxLength(25)
            .IsRequired();
        builder
            .Property(x => x.ModulKod)
            .IsRequired();
        builder
            .Property(x => x.Felelos)
            .IsRequired();
        builder
            .Property(x => x.MvTipus)
            .IsRequired();
        builder
            .Property(x => x.Labor)
            .IsRequired();
        builder
            .Property(x => x.MvOk)
            .IsRequired();
        builder
            .Property(x => x.MvOkaEgyeb)
            .HasMaxLength(255)
            .IsRequired(false);
        builder
            .Property(x => x.MvhKod)
            .IsRequired();
        builder
            .Property(x => x.MvHely)
            .HasMaxLength(255)
            .IsRequired(false);
        builder
            .Property(x => x.AkkrMintavetel)
            .IsRequired(false);
        builder
            .Property(x => x.Mintavevo)
            .IsRequired(false);
        builder
            .Property(x => x.MvDatum)
            .IsRequired();
        builder
            .Property(x => x.MintaAtvetel)
            .IsRequired();
        builder
            .Property(x => x.VizsgalatKezdete)
            .IsRequired();
        builder
            .Property(x => x.VizsgalatVege)
            .IsRequired();
        builder
            .Property(x => x.HUMVIexport)
            .IsRequired();

        builder.HasOne(e => e.cAkkrMintavetel)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => e.AkkrMintavetel)
            .HasPrincipalKey(m => m.Id)
            .OnDelete(DeleteBehavior.Restrict); //T�rl�s eset�n ne t�r�lje a kapcsol�d� m�rt�kegys�get

        builder.HasOne(e => e.cHUMVIfelelos)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => e.Felelos)
            .HasPrincipalKey(m => m.Id)
            .OnDelete(DeleteBehavior.Restrict); //T�rl�s eset�n ne t�r�lje a kapcsol�d� m�rt�kegys�get

        builder.HasOne(e => e.cHUMVImodul)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => e.ModulKod)
            .HasPrincipalKey(m => m.Id)
            .OnDelete(DeleteBehavior.Restrict); //T�rl�s eset�n ne t�r�lje a kapcsol�d� m�rt�kegys�get

        builder.HasOne(e => e.cMintavevo)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => e.Mintavevo)
            .HasPrincipalKey(m => m.Id)
            .OnDelete(DeleteBehavior.Restrict); //T�rl�s eset�n ne t�r�lje a kapcsol�d� m�rt�kegys�get

        builder.HasOne(e => e.cMvHely)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => e.MvhKod)
            .HasPrincipalKey(m => m.Id)
            .OnDelete(DeleteBehavior.Restrict); //T�rl�s eset�n ne t�r�lje a kapcsol�d� m�rt�kegys�get

        builder.HasOne(e => e.cMvOka)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => e.MvOk)
            .HasPrincipalKey(m => m.Id)
            .OnDelete(DeleteBehavior.Restrict); //T�rl�s eset�n ne t�r�lje a kapcsol�d� m�rt�kegys�get

        builder.HasOne(e => e.cMvTipus)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => e.MvTipus)
            .HasPrincipalKey(m => m.Id)
            .OnDelete(DeleteBehavior.Restrict); //T�rl�s eset�n ne t�r�lje a kapcsol�d� m�rt�kegys�get

        builder.HasOne(e => e.cVizsgaloLabor)
            .WithMany(m => m.Minta)
            .HasForeignKey(e => e.Labor)
            .HasPrincipalKey(m => m.Id)
            .OnDelete(DeleteBehavior.Restrict); //T�rl�s eset�n ne t�r�lje a kapcsol�d� m�rt�kegys�get




    }
}