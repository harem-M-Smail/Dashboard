using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoAPI.Models;

public class Product
{
    [Key]
    public int Id { get; init; }
    [JsonIgnore]
    public int UserId { get; init; }
    [MaxLength(50)]
    public required string Name { get; set; }
    [MaxLength(150)]
    public required string Description { get; set; }
    [DataType(DataType.Currency), Range(0, double.MaxValue)]
    public required double Price { get; set; }
    [Range(0, int.MaxValue)]
    public required uint Quantity { get; set; }
}