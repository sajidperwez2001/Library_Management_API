using Library.Api.DTOs;

namespace Library.Api.Services;

public interface IBookService
{
    Task<IEnumerable<BookReadDto>> GetAllAsync();
    Task<BookReadDto?> GetByIdAsync(int id);
    Task<BookReadDto> CreateAsync(BookCreateDto dto);
    Task<bool> UpdateAsync(int id, BookCreateDto dto);
    Task<bool> DeleteAsync(int id);
}
