using Library.Api.Data;
using Library.Api.DTOs;
using Library.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Services;

public class BookService : IBookService
{
    private readonly LibraryContext _db;
    public BookService(LibraryContext db) { _db = db; }

    public async Task<IEnumerable<BookReadDto>> GetAllAsync()
    {
        return await _db.Books.Select(b => new BookReadDto(b.Id, b.ISBN, b.Title, b.Author, b.Publisher, b.Year, b.TotalCopies, b.AvailableCopies)).ToListAsync();
    }

    public async Task<BookReadDto?> GetByIdAsync(int id)
    {
        var b = await _db.Books.FindAsync(id);
        if (b == null) return null;
        return new BookReadDto(b.Id, b.ISBN, b.Title, b.Author, b.Publisher, b.Year, b.TotalCopies, b.AvailableCopies);
    }

    public async Task<BookReadDto> CreateAsync(BookCreateDto dto)
    {
        var book = new Book
        {
            ISBN = dto.ISBN,
            Title = dto.Title,
            Author = dto.Author,
            Publisher = dto.Publisher,
            Year = dto.Year,
            TotalCopies = dto.TotalCopies,
            AvailableCopies = dto.TotalCopies
        };
        _db.Books.Add(book);
        await _db.SaveChangesAsync();
        return new BookReadDto(book.Id, book.ISBN, book.Title, book.Author, book.Publisher, book.Year, book.TotalCopies, book.AvailableCopies);
    }

    public async Task<bool> UpdateAsync(int id, BookCreateDto dto)
    {
        var book = await _db.Books.FindAsync(id);
        if (book == null) return false;
        book.ISBN = dto.ISBN;
        book.Title = dto.Title;
        book.Author = dto.Author;
        book.Publisher = dto.Publisher;
        book.Year = dto.Year;
        book.TotalCopies = dto.TotalCopies;
        book.AvailableCopies = Math.Min(book.AvailableCopies, dto.TotalCopies);
        book.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _db.Books.FindAsync(id);
        if (book == null) return false;
        _db.Books.Remove(book);
        await _db.SaveChangesAsync();
        return true;
    }
}
