using Persistence.Configurations;

namespace Persistence;

public class LaborDbContext : DbContext
{
    public DbSet<AkkrMintavetel> AkkrMintavetel { get; set; }
    public DbSet<Eredmeny> Eredmeny { get; set; }
    public DbSet<HUMVIfelelos> HumviFelelos { get; set; }
    public DbSet<HUMVImodul> HumviModul { get; set; }
    public DbSet<Mertekegyseg> Mertekegyseg { get; set; }
    public DbSet<Minta> Minta { get; set; }
    public DbSet<Mintavevo> Mintavevo { get; set; }
    public DbSet<MvHely> MvHely { get; set; }
    public DbSet<MvOka> MvOka { get; set; }
    public DbSet<MvTipus> MvTipus { get; set; }
    public DbSet<Parameter> Parameter { get; set; }
    public DbSet<VizsgaloLabor> VizsgaloLabor { get; set; }

    public LaborDbContext(DbContextOptions<LaborDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new AkkrMintavetelConfiguration().Configure(modelBuilder.Entity<AkkrMintavetel>());
        new EredmenyConfiguration().Configure(modelBuilder.Entity<Eredmeny>());
        new HUMVIfelelosConfiguration().Configure(modelBuilder.Entity<HUMVIfelelos>());
        new HUMVImodulConfiguration().Configure(modelBuilder.Entity<HUMVImodul>());
        new MertekegysegConfiguration().Configure(modelBuilder.Entity<Mertekegyseg>());
        new MintaConfiguration().Configure(modelBuilder.Entity<Minta>());
        new MintavevoConfiguration().Configure(modelBuilder.Entity<Mintavevo>());
        new MvHelyConfiguration().Configure(modelBuilder.Entity<MvHely>());
        new MvOkaConfiguration().Configure(modelBuilder.Entity<MvOka>());
        new MvTipusConfiguration().Configure(modelBuilder.Entity<MvTipus>());
        new ParameterConfiguration().Configure(modelBuilder.Entity<Parameter>());
        new VizsgaloLaborConfiguration().Configure(modelBuilder.Entity<VizsgaloLabor>());
    }
}