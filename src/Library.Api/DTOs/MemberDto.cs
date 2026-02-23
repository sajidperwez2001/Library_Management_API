namespace Library.Api.DTOs;

public record MemberCreateDto(
    string FullName,
    string? Email,
    string? Phone,
    string? Address,
    bool IsActive = true
);

public record MemberReadDto(
    int Id,
    string FullName,
    string? Email,
    string? Phone,
    string? Address,
    DateTime MembershipDate,
    bool IsActive
);
