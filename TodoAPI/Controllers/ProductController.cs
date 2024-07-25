using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.DTOs;
using TodoAPI.Services;
using TodoAPI.User_Claims;

namespace TodoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly UserClaims _userClaims;

    public ProductController(IProductService productService, IHttpContextAccessor contextAccessor)
    {
        _productService = productService;
        _userClaims = new UserClaims(contextAccessor.HttpContext!.User);
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await _productService.GetAllProducts(_userClaims.UserId));
    }

    [HttpGet("product-page/{page:int}/{pageSize:int}")]
    public IActionResult GetProductsByPage(int page, int pageSize)
    {
        return Ok(_productService.GetProductByPage(_userClaims.UserId, page, pageSize));
    }

    [HttpGet("product/{productId:int}")]
    public async Task<IActionResult> GetTodo(int productId)
    {
        var product = await _productService.GetProduct(_userClaims.UserId, productId);

        if (product is null)
            return NotFound();
        return Ok(product);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct(ProductDto newProduct)
    {
        await _productService.CreateProduct(_userClaims.UserId, newProduct);
        return NoContent();
    }

    [HttpPut("update/{productId:int}")]
    public async Task<IActionResult> UpdateProduct(int productId, ProductDto updateProduct)
    {
        var updatedProduct = await _productService.UpdateProduct(_userClaims.UserId, productId, updateProduct);

        if (updateProduct is null)
            return BadRequest("Invalid product id");

        return Ok(updateProduct);
    }

    [HttpDelete("product/{productId:int}")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        await _productService.DeleteProduct(_userClaims.UserId, productId);

        return NoContent();
    }
    
}