using Microsoft.EntityFrameworkCore;
using TodoAPI.Data_Context;
using TodoAPI.DTOs;
using TodoAPI.Models;

namespace TodoAPI.Services;

public class ProductService : IProductService
{
    private readonly TodoDbContext _database;

    public ProductService(TodoDbContext dbContext)
    {
        _database = dbContext;
    }
    
    public async Task CreateProduct(int userId, ProductDto newProduct)
    {
        var product = new Product()
        {
            UserId = userId,
            Name = newProduct.Name,
            Description = newProduct.Description,
            Price = newProduct.Price,
            Quantity = newProduct.Quantity
        };
        await _database.Products.AddAsync(product);
        await UpdateProductCount(userId, true);
        await _database.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAllProducts(int userId)
    {
        var products = await _database.Products
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.UserId)
            .ToListAsync();
        
        return products;
    }

    public ProductPage GetProductByPage(int userId, int page, int pageSize, string? name)
    {
        var products = GetAllProducts(userId).Result;

        if (string.IsNullOrWhiteSpace(name) is false)
        {
            products = products.Where(p => p.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        var productPage = new ProductPage()
        {
            TotalPages = (int)Math.Ceiling(products.Count / (double)pageSize), // calculate total page
            CurrentPage = page,
            Products = products.Skip(--page * pageSize).Take(pageSize).ToList()
        };

        return productPage;
    }

    public async Task<Product?> GetProduct(int userId, int productId)
    {
        var product = await _database.Products.FirstOrDefaultAsync(p => p.UserId == userId && p.Id == productId);

        return product;
    }

    public async Task<Product?> UpdateProduct(int userId, int productId, ProductDto productUpdateDto)
    {
        var product = GetProduct(userId, productId).Result;

        if (product is null)
            return null;

        product.Name = productUpdateDto.Name;
        product.Description = productUpdateDto.Description;
        product.Price = productUpdateDto.Price;
        product.Quantity = productUpdateDto.Quantity;

        var newProduct = _database.Products.Update(product);
        await _database.SaveChangesAsync();

        return newProduct.Entity;
    }

    public async Task DeleteProduct(int userId, int productId)
    {
        var product = GetProduct(userId, productId).Result;

        if (product is not null)
        {
            _database.Products.Remove(product);
            await UpdateProductCount(userId);
            await _database.SaveChangesAsync();
        }
    }

    private async Task UpdateProductCount(int userId, bool increment = false)
    {
        var user = await _database.Users.FindAsync(userId);
        if (increment)
            user!.ProductCount += 1;
        else
            user!.ProductCount -= 1;

        _database.Users.Update(user);
    }
}