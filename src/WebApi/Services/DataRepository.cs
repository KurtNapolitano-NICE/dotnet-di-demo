using System.Text.Json;

namespace WebApi.Services;

public interface IDataRepository
{
    int StoreValueReturnNext(int value);
}

public class DataRepository : IDataRepository
{
    private readonly INextValueService _nextValueService;

    public DataRepository(INextValueService nextValueService)
    {
        _nextValueService = nextValueService;
    }

    public int StoreValueReturnNext(int value)
    {
        var next = _nextValueService.FetchNextValue();

        var entry = new StoredEntryDto(DateTime.UtcNow, value, next);

        File.AppendAllLines("data.txt", new[] { JsonSerializer.Serialize(entry) });

        return next;
    }

    private record StoredEntryDto(DateTime InsertDate, int StoredValue, int NextValue);
}