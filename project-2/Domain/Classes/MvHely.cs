namespace Domain;

public class MvHely : Entity
{
    public string MvhKod { get; set; }
    public string NevSajat { get; set; }
    public string NevTeljes { get; set; }
    public string VizBazis { get; set; }
    public string NevRovid { get; set; }
    public string Telepules { get; set; }
    public string Tipus { get; set; }
    public string HumviRegiNev { get; set; }
    public float? GPS_N_Y { get; set; }
    public float? GPS_E_X { get; set; }
}