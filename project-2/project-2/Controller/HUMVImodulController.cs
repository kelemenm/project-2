namespace project_2.Controllers;

[Route("[controller]")]
public class HUMVIModulController(IRepository repository) : BaseController<cHUMVImodul, HUMVImodulDto>(repository)
{
    protected override cHUMVImodul CreateEntity(HUMVImodulDto dto)
    {
        return new cHUMVImodul
        {
            ModulKod = dto.ModulKod,
            Leiras = dto.Leiras,
        };
    }

    protected override cHUMVImodul UpdateEntity(cHUMVImodul entity, HUMVImodulDto dto)
    {
        entity.ModulKod = dto.ModulKod;
        entity.Leiras = dto.Leiras;

        return entity;
    }
}
