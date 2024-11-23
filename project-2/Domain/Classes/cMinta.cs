﻿namespace Domain;

public class cMinta : cEntity
{
    public string LaborMintaKod { get; set; }
    public long? ModulKod { get; set; }
    public long? Felelos { get; set; }
    public string? MvTipus { get; set; }
    public DateTime MvDatum { get; set; }
    public long? Labor { get; set; }
    public DateTime MintaAtvetel { get; set; }
    public DateTime VizsgalatKezdete { get; set; }
    public DateTime VizsgalatVege { get; set; }
    public string? MvOk { get; set; }
    public string MvOkaEgyeb { get; set; }
    public string MvhKod { get; set; }
    public string? MvHely { get; set; }
    public long? AkkrMintavetel { get; set; }
    public long? Mintavevo { get; set; }  
    public bool HUMVIexport { get; set; }

      
    public virtual cAkkrMintavetel? cAkkrMintavetel { get; set; }
    public virtual cHUMVIfelelos? cHUMVIfelelos { get; set; }
    public virtual cHUMVImodul? cHUMVImodul { get; set; }
    public virtual cMintavevo? cMintavevo { get; set; }
    public virtual cMvHely? cMvHely { get; set; }
    public virtual cMvOka? cMvOka { get; set; }
    public virtual cMvTipus? cMvTipus { get; set; }
    public virtual cVizsgaloLabor? cVizsgaloLabor { get; set; }
  
}