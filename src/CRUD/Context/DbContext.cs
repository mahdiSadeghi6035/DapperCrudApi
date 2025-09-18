using Microsoft.Data.SqlClient;

namespace CRUD.Context;

public class DbContext
{
    private readonly IConfiguration _configuration;

    public DbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public SqlConnection NewConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("Default"));
    }

    public SqlConnection NewMasterConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("Master"));
    }
}
