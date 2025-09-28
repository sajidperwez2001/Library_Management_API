using Xunit;
using Library.Api.Services;
using Library.Tests.TestHelpers;
using Library.Api.Data;
using Library.Api.Models;

namespace Library.Tests;

public class LoanServiceTests
{
    [Fact]
    public async Task BorrowBook_DecrementsAvailable()
    {
        using var ctx = DbContextFactory.CreateInMemoryContext("loan_test1");
        var book = new Book { ISBN = "X1", Title = "B1", TotalCopies = 2, AvailableCopies = 2 };
        var member = new Member { FullName = "M1" };
        ctx.Books.Add(book); ctx.Members.Add(member); await ctx.SaveChangesAsync();

        var svc = new LoanService(ctx);
        var loan = await svc.BorrowAsync(book.Id, member.Id, 7);
        Assert.NotNull(loan);
        var b = await ctx.Books.FindAsync(book.Id);
        Assert.Equal(1, b!.AvailableCopies);
    }

    [Fact]
    public async Task ReturnBook_IncrementsAvailable()
    {
        using var ctx = DbContextFactory.CreateInMemoryContext("loan_test2");
        var book = new Book { ISBN = "X2", Title = "B2", TotalCopies = 1, AvailableCopies = 1 };
        var member = new Member { FullName = "M2" };
        ctx.Books.Add(book); ctx.Members.Add(member); await ctx.SaveChangesAsync();
        var svc = new LoanService(ctx);
        var loan = await svc.BorrowAsync(book.Id, member.Id, 1);
        Assert.NotNull(loan);
        var ret = await svc.ReturnAsync(loan!.Id);
        Assert.NotNull(ret);
        var b = await ctx.Books.FindAsync(book.Id);
        Assert.Equal(1, b!.AvailableCopies);
    }
}
