using System.ComponentModel.DataAnnotations;

namespace Library.Api.Models;

public class Book
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string ISBN { get; set; } = null!;
    [Required, MaxLength(250)]
    public string Title { get; set; } = null!;
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public int? Year { get; set; }
    public int TotalCopies { get; set; } = 1;
    public int AvailableCopies { get; set; } = 1;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
