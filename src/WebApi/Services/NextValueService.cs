namespace WebApi.Services;

public interface INextValueService
{
    int FetchNextValue();
}

public class NextValueService : INextValueService
{
    private int _currentValue;

    public NextValueService()
    {
        _currentValue = 0;
    }

    public int FetchNextValue()
    {
        _currentValue++;
        return _currentValue;
    }
}

public class DbNextValueService : INextValueService
{
    public DbNextValueService()
    {
    }

    public int FetchNextValue()
    {
        //using (var connection = new MssqlConnection(whatever))
        //{
        //    return connection.Query<int>("select next_id from table");
        //}
        return 0;
    }
}