namespace WebApi.Services;

public interface INextValueService
{
    int FetchNextValue();
}

public class NextNextValueService : INextValueService
{
    private int _currentValue;

    public NextNextValueService()
    {
        _currentValue = 0;
    }

    public NextNextValueService(int initialValue)
    {
        _currentValue = initialValue;
    }

    public int FetchNextValue()
    {
        _currentValue++;
        return _currentValue;
    }
}