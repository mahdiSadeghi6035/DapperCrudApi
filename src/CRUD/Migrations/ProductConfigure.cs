using Dapper;
using Microsoft.Data.SqlClient;

namespace CRUD.Migrations;

public class ProductConfigure
{
    public static void CreateProduct(SqlConnection sqlConnection)
    {
        string query = """
                IF OBJECT_ID('dbo.Product', 'U') IS NULL
                BEGIN
                CREATE TABLE dbo.Product(
                    Id INT IDENTITY(100,100) NOT NULL,
                    [Name] NVARCHAR(50) UNIQUE NOT NULL,
                    UnitPrice MONEY NOT NULL,
                    CreateDate DATETIME NOT NULL DEFAULT GETDATE()
                );
                END
            """;
        sqlConnection.Execute(query);
    }
}
