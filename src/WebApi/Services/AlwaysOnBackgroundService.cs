namespace WebApi.Services;

public class AlwaysOnBackgroundService : BackgroundService
{
    private readonly ILogger<AlwaysOnBackgroundService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AlwaysOnBackgroundService(ILogger<AlwaysOnBackgroundService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("BackgroundService - ExecuteAsync Begin");
        while (!stoppingToken.IsCancellationRequested)
        {

            await Task.Delay(5000, stoppingToken);

            using var scope = _serviceScopeFactory.CreateScope();

            var worker = scope.ServiceProvider.GetRequiredService<IBackgroundWorker>();
            worker.DoWork();

        }
        _logger.LogInformation("BackgroundService - ExecuteAsync End");
    }
    
}

public interface IBackgroundWorker
{
    void DoWork();
}

public class BackgroundWorker : IBackgroundWorker
{
    private readonly ILogger<BackgroundWorker> _logger;
    private readonly INextValueService _nextValueService;

    public BackgroundWorker(ILogger<BackgroundWorker> logger, INextValueService nextValueService)
    {
        _logger = logger;
        _nextValueService = nextValueService;
    }

    public void DoWork()
    {

        var val = _nextValueService.FetchNextValue();
        _logger.LogInformation("BackgroundService fetched {val}", val);
    }
}