namespace project_2.Controllers;

[Route("[controller]")]
public class VizsgaloLaborController(IRepository repository) : BaseController<cVizsgaloLabor, VizsgaloLaborDto>(repository)
{
    protected override cVizsgaloLabor CreateEntity(VizsgaloLaborDto dto)
    {
        return new cVizsgaloLabor
        {
            Labor = dto.Labor,
            LabAkkrSzam = dto.LabAkkrSzam,
            Nev = dto.Nev,
            Cim = dto.Cim,
            ErvKezdete = dto.ErvKezdete,
            ErvVege = dto.ErvVege
        };
    }

    protected override cVizsgaloLabor UpdateEntity(cVizsgaloLabor entity, VizsgaloLaborDto dto)
    {
        entity.Labor = dto.Labor;
        entity.LabAkkrSzam = dto.LabAkkrSzam;
        entity.Nev = dto.Nev;
        entity.Cim = dto.Cim;
        entity.ErvKezdete = dto.ErvKezdete;
        entity.ErvVege = dto.ErvVege;

        return entity;
    }
}
