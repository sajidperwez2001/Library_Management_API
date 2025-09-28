namespace Library.Api.DTOs;

public record LoginRequest(string Username, string Password);
public record RegisterRequest(string Username, string Password, string Role);
public record AuthResponse(string Token);
