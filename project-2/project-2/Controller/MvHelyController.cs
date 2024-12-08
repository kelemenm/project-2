namespace project_2.Controllers;

[Route("[controller]")]
public class MvHelyController(IRepository repository) : BaseController<cMvHely, MvHelyDto>(repository)
{
    protected override cMvHely CreateEntity(MvHelyDto dto)
    {
        return new cMvHely
        {
            MvhKod = dto.MvhKod,
            NevSajat = dto.NevSajat,
            NevTeljes = dto.NevTeljes,
            VizBazis = dto.VizBazis,
            NevRovid = dto.NevRovid,
            Telepules = dto.Telepules,
            Tipus = dto.Tipus,
            HumviRegiNev = dto.HumviRegiNev,
            GPS_N_Y = dto.GPS_N_Y,
            GPS_E_X = dto.GPS_E_X,
        };
    }

    protected override cMvHely UpdateEntity(cMvHely entity, MvHelyDto dto)
    {
        entity.MvhKod = dto.MvhKod;
        entity.NevSajat = dto.NevSajat;
        entity.NevTeljes = dto.NevTeljes;
        entity.VizBazis = dto.VizBazis;
        entity.NevRovid = dto.NevRovid;
        entity.Telepules = dto.Telepules;
        entity.Tipus = dto.Tipus;
        entity.HumviRegiNev = dto.HumviRegiNev;
        entity.GPS_N_Y = dto.GPS_N_Y;
        entity.GPS_E_X = dto.GPS_E_X;

        return entity;
    }
}
