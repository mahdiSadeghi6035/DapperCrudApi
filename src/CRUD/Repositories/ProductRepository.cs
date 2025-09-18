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
        var newConnection = _dbContext.NewConnection();
        await newConnection.OpenAsync();
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
        var newConnection = _dbContext.NewConnection();
        await newConnection.OpenAsync();
        string query = """
            DELETE FROM dbo.Product
            WHERE Id = @Id
            """;
        var result = await newConnection.ExecuteAsync(query, new { id = id });

        return result == 1 ? OperationResult.Success() : OperationResult.Failed("Failed operation.");
    }

    public async Task<ProductModel?> DetailsAsync(int id)
    {
        var newConnection = _dbContext.NewConnection();
        await newConnection.OpenAsync();
        string query = """
            SELECT * FROM dbo.Product WHERE Id = @Id
            """;
        var result = await newConnection.QuerySingleOrDefaultAsync<ProductModel>(query, new { id = id });
        return result;
    }

    public async Task<List<ProductModel>> GetAllAsync()
    {
        var newConnection = _dbContext.NewConnection();
        await newConnection.OpenAsync();
        string query = """
            SELECT * FROM dbo.Product
            """;
        var result = await newConnection.QueryAsync<ProductModel>(query);
        return result.ToList();
    }

    public async Task<OperationResult> UpdateAsync(UpdateProductModel updateProductModel)
    {
        var newConnection = _dbContext.NewConnection();
        await newConnection.OpenAsync();
        string query = """
            UPDATE dbo.Product 
            SET Name = @Name, UnitPrice = @UnitPrice
            WHERE Id = @Id
            """;
        var result = await newConnection.ExecuteAsync(query, updateProductModel);

        return result == 1 ? OperationResult.Success() : OperationResult.Failed("Failed operation.");
    }
}
