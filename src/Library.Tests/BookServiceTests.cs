using Xunit;
using Library.Api.Services;
using Library.Tests.TestHelpers;
using Library.Api.Data;

namespace Library.Tests;

public class BookServiceTests
{
    [Fact]
    public async Task CreateBook_IncreasesCount()
    {
        using var ctx = DbContextFactory.CreateInMemoryContext("books_test1");
        var svc = new BookService(ctx);
        var dto = new Library.Api.DTOs.BookCreateDto("ISBN123","Test Book","Author",null,2020,3);
        var created = await svc.CreateAsync(dto);
        var all = await svc.GetAllAsync();
        Assert.Single(all);
        Assert.Equal("ISBN123", created.ISBN);
    }

    [Fact]
    public async Task UpdateBook_ChangesTitle()
    {
        using var ctx = DbContextFactory.CreateInMemoryContext("books_test2");
        var svc = new BookService(ctx);
        var dto = new Library.Api.DTOs.BookCreateDto("ISBN111","Old","A",null,2000,1);
        var created = await svc.CreateAsync(dto);
        var update = new Library.Api.DTOs.BookCreateDto("ISBN111","New Title","A",null,2000,1);
        var ok = await svc.UpdateAsync(created.Id, update);
        Assert.True(ok);
        var fetched = await svc.GetByIdAsync(created.Id);
        Assert.Equal("New Title", fetched!.Title);
    }
}
