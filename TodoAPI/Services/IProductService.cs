using TodoAPI.DTOs;
using TodoAPI.Models;

namespace TodoAPI.Services;

public interface IProductService
{
    Task CreateProduct(int userId, ProductDto newProduct);
    Task<List<Product>> GetAllProducts(int userId);
    ProductPage GetProductByPage(int userId, int page, int pageSize);
    Task<Product?> GetProduct(int userId, int productId);
    Task<Product?> UpdateProduct(int userId, int productId, ProductDto productUpdateDto);
    Task DeleteProduct(int userId, int productId);
}