using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DTOs;

public class ProductDto
{
    [MaxLength(50)]
    public required string Name { get; set; }
    [MaxLength(150)]
    public required string Description { get; set; }
    [DataType(DataType.Currency), Range(0, double.MaxValue)]
    public required double Price { get; set; }
    [Range(0, int.MaxValue)]
    public required uint Quantity { get; set; }
}