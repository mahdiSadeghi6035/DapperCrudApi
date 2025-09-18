using CRUD.Interfaces;
using CRUD.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers.V1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddProductModel addProductModel)
    {
        var result = await _productRepository.AddAsync(addProductModel);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductModel updateProductModel)
    {
        var result = await _productRepository.UpdateAsync(updateProductModel);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _productRepository.DeleteAsync(id);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> DetailsAsync(int id)
    {
        var result = await _productRepository.GetByIdAsync(id);
        return result is not null ? Ok(result) : NotFound(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _productRepository.GetAllAsync();
        return Ok(result);
    }
}
