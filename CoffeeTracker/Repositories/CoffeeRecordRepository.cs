using SqlKata;
using SqlKata.Execution;
using Npgsql;
using SqlKata.Compilers;

namespace CoffeeTracker.Repositories;

public class CoffeeRecordRepository(NpgsqlConnection connection)
{
    private readonly QueryFactory _QueryFactory = new QueryFactory(connection, new SqlServerCompiler());

    public int InsertCoffeeRecord(CoffeeRecord record) 
    {

        return _QueryFactory.Query("records").InsertGetId<int>(new {
            userid = record.UserId,
            timeofconsumption = record.TimeOfConsumption,
            coffeetype = record.CoffeeType,
            location = record.Location
        });
    }
}