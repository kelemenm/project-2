namespace project_2.Controllers;

[Route("[controller]")]
public class AkkrMintavetelController(IRepository repository) : BaseController<cAkkrMintavetel, AkkrMintavetelDto>(repository)
{
    protected override cAkkrMintavetel CreateEntity(AkkrMintavetelDto dto)
    {
        return new cAkkrMintavetel
        {
            AkkrMintavetelStatusz = dto.AkkrMintavetelStatusz,
            Leiras = dto.Leiras,
        };
    }

    protected override cAkkrMintavetel UpdateEntity(cAkkrMintavetel entity, AkkrMintavetelDto dto)
    {
        entity.AkkrMintavetelStatusz = dto.AkkrMintavetelStatusz;
        entity.Leiras = dto.Leiras;

        return entity;
    }
}