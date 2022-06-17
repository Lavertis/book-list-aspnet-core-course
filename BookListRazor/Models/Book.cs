using System.ComponentModel.DataAnnotations;

namespace BookListRazor.Models;

public class Book
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    public string? Author { get; set; } = string.Empty;
    public string? ISBN { get; set; } = string.Empty;
}