using CRUD.Common;
using CRUD.Model;

namespace CRUD.Interfaces;

public interface IProductRepository
{
    Task<OperationResult> AddAsync(AddProductModel addProductModel);
    Task<OperationResult> DeleteAsync(int id);
    Task<OperationResult> UpdateAsync(UpdateProductModel updateProductModel);
    Task<ProductModel?> GetByIdAsync(int id);
    Task<List<ProductModel>> GetAllAsync();
}
