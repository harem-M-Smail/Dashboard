using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoAPI.Models;

public class Todo
{
    [Key]
    public int Id { get; init; }
    [JsonIgnore]
    public int UserId { get; init; }
    [MaxLength(50)]
    public required string Title { get; set; }
    [MaxLength(150), DataType(DataType.MultilineText)]
    public required string Description { get; set; }
    [MaxLength(15)]
    public required string Status { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime Created { get; init; } = DateTime.Now.AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond);
    [DataType(DataType.DateTime)]
    public DateTime Updated { get; set; } = DateTime.Now.AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond);
}