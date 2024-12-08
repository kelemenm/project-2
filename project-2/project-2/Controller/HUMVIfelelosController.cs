namespace project_2.Controllers;

[Route("[controller]")]
public class HUMVIfelelosController(IRepository repository) : BaseController<cHUMVIfelelos, HUMVIfelelosDto>(repository)
{
    protected override cHUMVIfelelos CreateEntity(HUMVIfelelosDto dto)
    {
        return new cHUMVIfelelos
        {
            Felelos = dto.Felelos,
            Nev = dto.Nev,
            Cim = dto.Cim,
        };
    }

    protected override cHUMVIfelelos UpdateEntity(cHUMVIfelelos entity, HUMVIfelelosDto dto)
    {
        entity.Felelos = dto.Felelos;
        entity.Nev = dto.Nev;
        entity.Cim = dto.Cim;

        return entity;
    }
}
