using CRUD.Context;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CRUD.Migrations;

public static class Migration
{
    public static void ApplyMigration(this WebApplication webApplication)
    {
        var services = webApplication.Services.CreateScope().ServiceProvider;
        var context = services.GetRequiredService<DbContext>();
        using var newConnection = context.NewConnection();
        using var newMasterConnection = context.NewMasterConnection();

        CreateDatabase(newMasterConnection);
        
        ProductConfigure.CreateProduct(newConnection);
    }

    public static void CreateDatabase(SqlConnection sqlConnection)
    {
        string query = """
                IF DB_ID('dapper') IS NULL
                BEGIN
                    CREATE DATABASE dapper;
                END
            """;
        sqlConnection.Execute(query);
    }
}