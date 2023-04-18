namespace WebApi.Services;

public class AlwaysOnBackgroundService : BackgroundService
{
    private readonly ILogger<AlwaysOnBackgroundService> _logger;
    private readonly INextValueService _nextValueService;

    public AlwaysOnBackgroundService(ILogger<AlwaysOnBackgroundService> logger, INextValueService nextValueService)
    {
        _logger = logger;
        _nextValueService = nextValueService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("BackgroundService - ExecuteAsync Begin");
        while (!stoppingToken.IsCancellationRequested)
        {

            await Task.Delay(5000, stoppingToken);

            var val = _nextValueService.FetchNextValue();
            _logger.LogInformation("BackgroundService fetched {val}", val);

        }
        _logger.LogInformation("BackgroundService - ExecuteAsync End");
    }
}