namespace WebApi.Services;

public interface ICalculationService
{
    int CalculateStartPlusNext(int start);
}

public class CalculationService : ICalculationService
{
    private readonly INextValueService _nextValueService;

    public CalculationService(INextValueService nextValueService)
    {
        _nextValueService = nextValueService;
    }

    public int CalculateStartPlusNext(int start)
    {
        return start + _nextValueService.FetchNextValue();
    }
}