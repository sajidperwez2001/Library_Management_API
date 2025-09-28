using System.ComponentModel.DataAnnotations;

namespace Library.Api.Models;

public class User
{
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Username { get; set; } = null!;
    [Required]
    public string PasswordHash { get; set; } = null!; // Demo only
    [Required]
    public string Role { get; set; } = "Member";
}
