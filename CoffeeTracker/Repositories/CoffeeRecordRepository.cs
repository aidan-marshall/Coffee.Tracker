using SqlKata.Execution;
using Npgsql;
using SqlKata.Compilers;

namespace CoffeeTracker.Repositories;

public class CoffeeRecordRepository(NpgsqlConnection connection)
{
    private readonly QueryFactory _queryFactory = new (connection, new PostgresCompiler());

    public int InsertCoffeeRecord(CoffeeRecord record) 
    {

        return _queryFactory.Query("records").InsertGetId<int>(new {
            user_id = record.UserId,
            time_of_consumption = record.TimeOfConsumption,
            coffee_type = record.CoffeeType,
            location = record.Location
        });
    }
}