using Library.Api.Models;
namespace Library.Api.Services;
public interface ILoanService
{
    Task<Loan?> BorrowAsync(int bookId, int memberId, int days);
    Task<Loan?> ReturnAsync(int loanId);
    Task<IEnumerable<Loan>> GetAllAsync();
}
