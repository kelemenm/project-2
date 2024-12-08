namespace project_2.Controllers;

[Route("[contoller]")]
public class MvTipusController(IRepository repository) : BaseController<cMvTipus, MvTipusDto>(repository)
{
    protected override cMvTipus CreateEntity(MvTipusDto dto)
    {
        return new cMvTipus
        {
            MvTipusNev = dto.MvTipusNev,
            Leiras = dto.Leiras,
        };
    }

    protected override cMvTipus UpdateEntity(cMvTipus entity, MvTipusDto dto)
    {
        entity.MvTipusNev = dto.MvTipusNev;
        entity.Leiras = dto.Leiras;

        return entity;
    }
}
