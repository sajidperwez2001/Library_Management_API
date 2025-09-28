using System.ComponentModel.DataAnnotations;

namespace Library.Api.Models;

public class Member
{
    public int Id { get; set; }
    [Required, MaxLength(200)]
    public string FullName { get; set; } = null!;
    [EmailAddress]
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime MembershipDate { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}
