using Microsoft.EntityFrameworkCore;
using Library.Api.Data;

namespace Library.Tests.TestHelpers;

public static class DbContextFactory
{
    public static LibraryContext CreateInMemoryContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        return new LibraryContext(options);
    }
}
