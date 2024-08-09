using SqlKata;
using SqlKata.Execution;
using System.Data.SqlClient;

public class CoffeeRecordRepository()
{
    private readonly var _connection = new SqlConnection("Data Source=CoffeeTracker;User Id=postgres;Password=random");
    private readonly var _compiler = new SqlServerCompiler();

    var _db new QueryFactory(_connection, _compiler);

    
}
