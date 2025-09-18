using CRUD.Common;
using CRUD.Context;
using CRUD.Interfaces;
using CRUD.Model;
using Dapper;

namespace CRUD.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DbContext _dbContext;

    public ProductRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult> AddAsync(AddProductModel addProductModel)
    {
        await using var newConnection = _dbContext.NewConnection();

        string existQuery = "SELECT COUNT(*) FROM dbo.Product WHERE Name = @name";
        var exists = await newConnection.QueryFirstOrDefaultAsync<int>(existQuery, new { name = addProductModel.Name });
        if (exists > 0)
            return OperationResult.Failed("Duplicate record.");

        string query = """
            INSERT INTO dbo.Product ([Name],UnitPrice)
            VALUES
            (@Name, @UnitPrice)
            """;
        var result = await newConnection.ExecuteAsync(query, addProductModel);

        return result == 1 ? OperationResult.Success() : OperationResult.Failed("Failed operation.");
    }
    public async Task<OperationResult> DeleteAsync(int id)
    {
        await using var newConnection = _dbContext.NewConnection();
        string query = """
            DELETE FROM dbo.Product
            WHERE Id = @Id
            """;
        var result = await newConnection.ExecuteAsync(query, new { id = id });
        return result == 1 ? OperationResult.Success() : OperationResult.Failed("Failed operation.");
    }

    public async Task<ProductModel?> GetByIdAsync(int id)
    {
        await using var newConnection = _dbContext.NewConnection();
        string query = """
            SELECT * FROM dbo.Product WHERE Id = @Id
            """;
        var result = await newConnection.QuerySingleOrDefaultAsync<ProductModel>(query, new { id = id });
        return result;
    }

    public async Task<List<ProductModel>> GetAllAsync()
    {
        await using var newConnection = _dbContext.NewConnection();
        string query = """
            SELECT * FROM dbo.Product
            """;
        var result = await newConnection.QueryAsync<ProductModel>(query);
        return result.ToList();
    }

    public async Task<OperationResult> UpdateAsync(UpdateProductModel updateProductModel)
    {
        await using var newConnection = _dbContext.NewConnection();

        string existQuery = "SELECT COUNT(*) FROM dbo.Product WHERE Name = @name AND Id <> @id";
        var exists = await newConnection.QueryFirstOrDefaultAsync<int>(existQuery, new { name = updateProductModel.Name, id = updateProductModel.Id });
        if (exists > 0)
            return OperationResult.Failed("Duplicate record.");

        string query = """
            UPDATE dbo.Product 
            SET Name = @Name, UnitPrice = @UnitPrice
            WHERE Id = @Id
            """;
        var result = await newConnection.ExecuteAsync(query, updateProductModel);

        return result == 1 ? OperationResult.Success() : OperationResult.Failed("Failed operation.");
    }
}
