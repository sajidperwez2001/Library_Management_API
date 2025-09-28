using Xunit;
using Library.Api.Services;
using Library.Tests.TestHelpers;

namespace Library.Tests;

public class AuthServiceTests
{
    [Fact]
    public async Task RegisterAndAuthenticate_Works()
    {
        using var ctx = Library.Tests.TestHelpers.DbContextFactory.CreateInMemoryContext("auth_test1");
        var auth = new Library.Api.Services.AuthService(ctx);
        var reg = await auth.RegisterAsync("u1","p1","Member");
        Assert.NotNull(reg);
        var user = await auth.AuthenticateAsync("u1","p1");
        Assert.NotNull(user);
        Assert.Equal("u1", user!.Username);
    }
}
