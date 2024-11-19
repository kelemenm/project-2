using Persistence.Configurations;

namespace Persistence;

public class LaborDbContext : DbContext
{
    public DbSet<cAkkrMintavetel> AkkrMintavetel { get; set; }
    public DbSet<cEredmeny> Eredmeny { get; set; }
    public DbSet<cHUMVIfelelos> HumviFelelos { get; set; }
    public DbSet<cHUMVImodul> HumviModul { get; set; }
    public DbSet<cMertekegyseg> Mertekegyseg { get; set; }
    public DbSet<cMinta> Minta { get; set; }
    public DbSet<cMintavevo> Mintavevo { get; set; }
    public DbSet<cMvHely> MvHely { get; set; }
    public DbSet<cMvOka> MvOka { get; set; }
    public DbSet<cMvTipus> MvTipus { get; set; }
    public DbSet<cParameter> Parameter { get; set; }
    public DbSet<cVizsgaloLabor> VizsgaloLabor { get; set; }

    public LaborDbContext(DbContextOptions<LaborDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new AkkrMintavetelConfiguration().Configure(modelBuilder.Entity<cAkkrMintavetel>());
        new EredmenyConfiguration().Configure(modelBuilder.Entity<cEredmeny>());
        new HUMVIfelelosConfiguration().Configure(modelBuilder.Entity<cHUMVIfelelos>());
        new HUMVImodulConfiguration().Configure(modelBuilder.Entity<cHUMVImodul>());
        new MertekegysegConfiguration().Configure(modelBuilder.Entity<cMertekegyseg>());
        new MintaConfiguration().Configure(modelBuilder.Entity<cMinta>());
        new MintavevoConfiguration().Configure(modelBuilder.Entity<cMintavevo>());
        new MvHelyConfiguration().Configure(modelBuilder.Entity<cMvHely>());
        new MvOkaConfiguration().Configure(modelBuilder.Entity<cMvOka>());
        new MvTipusConfiguration().Configure(modelBuilder.Entity<cMvTipus>());
        new ParameterConfiguration().Configure(modelBuilder.Entity<cParameter>());
        new VizsgaloLaborConfiguration().Configure(modelBuilder.Entity<cVizsgaloLabor>());
    }
}