namespace WebApi.Services;

public class StartupHostedService : IHostedService
{
    private readonly ILogger<StartupHostedService> _logger;
    private readonly INextValueService _nextValueService;

    public StartupHostedService(
        ILogger<StartupHostedService> logger,
        INextValueService nextValueService)
    {
        _logger = logger;
        _nextValueService = nextValueService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StartupService - StartAsync Begin");

        await Task.Delay(8000, cancellationToken);

        var val = _nextValueService.FetchNextValue();
        _logger.LogInformation("StartupService fetched {val}", val);

        _logger.LogInformation("StartupService - StartAsync End");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}