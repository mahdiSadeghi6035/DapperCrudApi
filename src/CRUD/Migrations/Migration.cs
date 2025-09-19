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
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Migration");

        using var newConnection = context.NewConnection();
        using var newMasterConnection = context.NewMasterConnection();


        TryApplyMigration(newMasterConnection, newConnection, logger);
    }

    public static bool TryApplyMigration(SqlConnection masterConnection, SqlConnection connection, ILogger logger, int tryMax = 50)
    {
        try
        {
            CreateDatabase(masterConnection);
            ProductConfigure.CreateProduct(connection);
        }
        catch (Exception ex)
        {
            if (tryMax == 0)
            {
                logger.LogCritical(ex, "Migration failed after max retries");
                return false;
            }
            logger.LogError(ex, $"Try to connect, remaining retries: {tryMax}");
            return TryApplyMigration(masterConnection, connection, logger, tryMax - 1);
        }

        logger.LogInformation("Migration applied successfully");
        return true;

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