namespace project_2.Controllers;

[Route("[controller]")]
public class ParameterController(IRepository repository) : BaseController<cParameter, ParameterDto>(repository)
{
    protected override cParameter CreateEntity(ParameterDto dto)
    {
        return new cParameter
        {
            ParKod = dto.ParKod,
            HumviLeiras = dto.HumviLeiras,
            SajatLeiras = dto.SajatLeiras,
            ParamErtek = dto.ParamErtek,
            ParamTip = dto.ParamTip
        };
    }

    protected override cParameter UpdateEntity(cParameter entity, ParameterDto dto)
    {
        entity.ParKod = dto.ParKod;
        entity.HumviLeiras = dto.HumviLeiras;
        entity.SajatLeiras = dto.SajatLeiras;
        entity.ParamErtek = dto.ParamErtek;
        entity.ParamTip = dto.ParamTip;

        return entity;
    }
}
