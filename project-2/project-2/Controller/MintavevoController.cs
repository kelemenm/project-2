namespace project_2.Controllers;

[Route("[contoller]")]
public class MintavevoController(IRepository repository) : BaseController<cMintavevo, MintavevoDto>(repository)
{
    protected override cMintavevo CreateEntity(MintavevoDto dto)
    {
        return new cMintavevo
        {
            MintavevoAzonosito = dto.MintavevoAzonosito,
            MvAkkrSzam = dto.MvAkkrSzam,
            Nev = dto.Nev,
            Cim = dto.Cim,
            ErvKezdete = dto.ErvKezdete,
            ErvVege = dto.ErvVege,
        };
    }

    protected override cMintavevo UpdateEntity(cMintavevo entity, MintavevoDto dto)
    {
        entity.MintavevoAzonosito = dto.MintavevoAzonosito;
        entity.MvAkkrSzam = dto.MvAkkrSzam;
        entity.Nev = dto.Nev;
        entity.Cim = dto.Cim;
        entity.ErvKezdete = dto.ErvKezdete;
        entity.ErvVege = dto.ErvVege;

        return entity;
    }
}
