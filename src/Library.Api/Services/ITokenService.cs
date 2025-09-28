using Library.Api.Models;
namespace Library.Api.Services; public interface ITokenService { string CreateToken(User user); }