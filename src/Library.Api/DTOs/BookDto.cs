namespace Library.Api.DTOs;

public record BookCreateDto(string ISBN, string Title, string? Author, string? Publisher, int? Year, int TotalCopies);
public record BookReadDto(int Id, string ISBN, string Title, string? Author, string? Publisher, int? Year, int TotalCopies, int AvailableCopies);
