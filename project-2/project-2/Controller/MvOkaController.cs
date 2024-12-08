namespace project_2.Controllers;

[Route("[controller]")]
public class MvOkaController(IRepository repository) : BaseController<cMvOka, MvOkaDto>(repository)
{
    protected override cMvOka CreateEntity(MvOkaDto dto)
    {
        return new cMvOka
        {
            MvOk = dto.MvOk,
            Leiras = dto.Leiras,
        };
    }

    protected override cMvOka UpdateEntity(cMvOka entity, MvOkaDto dto)
    {
        entity.MvOk = dto.MvOk;
        entity.Leiras = dto.Leiras;

        return entity;
    }
}
