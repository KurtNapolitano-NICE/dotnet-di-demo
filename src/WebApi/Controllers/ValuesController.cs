using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/values")]
public class ValuesController : ControllerBase
{

    private readonly ILogger<ValuesController> _logger;
    private readonly INextValueService _nextValueService;
    private readonly ICalculationService _calculationService;
    private readonly IDataRepository _dataRepository;

    public ValuesController(
        ILogger<ValuesController> logger,
        INextValueService nextValueService,
        ICalculationService calculationService,
        IDataRepository dataRepository)
    {
        _logger = logger;
        _nextValueService = nextValueService;
        _calculationService = calculationService;
        _dataRepository = dataRepository;
    }

    [HttpGet("next")]
    public IActionResult GetNextValue()
    {
        var val = _nextValueService.FetchNextValue();

        return Ok(val);
    }

    [HttpPost("next")]
    public IActionResult PostNextValue()
    {
        var start = _nextValueService.FetchNextValue();
        var sum = _calculationService.CalculateStartPlusNext(start);
        var result = _dataRepository.StoreValueReturnNext(sum);

        return Ok(result);
    }
}