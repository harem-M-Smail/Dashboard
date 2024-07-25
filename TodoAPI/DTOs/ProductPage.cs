using TodoAPI.Models;

namespace TodoAPI.DTOs;

public class ProductPage
{
    public required int TotalPages { get; init; }
    public required int CurrentPage { get; init; }
    
    public required List<Product> Products { get; init; }
}