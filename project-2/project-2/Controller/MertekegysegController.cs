namespace project_2.Controllers;

[Route("[contoller]")]
public class MertekegysegController(IRepository repository) : BaseController<cMertekegyseg, MertekegysegDto>(repository)
{
    protected override cMertekegyseg CreateEntity(MertekegysegDto dto)
    {
        return new cMertekegyseg
        {
            Megyseg = dto.Megyseg,
            HumviLeiras = dto.HumviLeiras,
            SajatLeiras = dto.SajatLeiras,
        };
    }

    protected override cMertekegyseg UpdateEntity(cMertekegyseg entity, MertekegysegDto dto)
    {
        entity.Megyseg = dto.Megyseg;
        entity.HumviLeiras = dto.HumviLeiras;
        entity.SajatLeiras = dto.SajatLeiras;

        return entity;
    }
}
