using Library.Api.Data;
using Library.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace Library.Api.Services;
public class LoanService : ILoanService
{
    private readonly LibraryContext _db;
    private const int MaxLoansPerMember = 5;
    public LoanService(LibraryContext db) { _db = db; }

    public async Task<Loan?> BorrowAsync(int bookId, int memberId, int days)
    {
        var book = await _db.Books.FindAsync(bookId);
        if (book == null || book.AvailableCopies <= 0) return null;
        var member = await _db.Members.FindAsync(memberId);
        if (member == null) return null;
        var activeLoans = await _db.Loans.CountAsync(l => l.MemberId == memberId && l.ReturnDate == null);
        if (activeLoans >= MaxLoansPerMember) return null;
        var loan = new Loan {
            BookId = bookId,
            MemberId = memberId,
            LoanDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(days),
            Status = LoanStatus.Borrowed
        };
        book.AvailableCopies -= 1;
        _db.Loans.Add(loan);
        await _db.SaveChangesAsync();
        return loan;
    }

    public async Task<Loan?> ReturnAsync(int loanId)
    {
        var loan = await _db.Loans.Include(l => l.Book).FirstOrDefaultAsync(l => l.Id == loanId);
        if (loan == null || loan.ReturnDate != null) return null;
        loan.ReturnDate = DateTime.UtcNow;
        if (loan.DueDate < loan.ReturnDate)
        {
            loan.Status = LoanStatus.Overdue;
            var daysLate = (loan.ReturnDate.Value - loan.DueDate).Days;
            loan.Fine = Math.Max(0, daysLate) * 1m;
        }
        else
        {
            loan.Status = LoanStatus.Returned;
        }
        if (loan.Book != null)
            loan.Book.AvailableCopies += 1;
        await _db.SaveChangesAsync();
        return loan;
    }

    public async Task<IEnumerable<Loan>> GetAllAsync()
    {
        return await _db.Loans.Include(l => l.Book).Include(l => l.Member).ToListAsync();
    }
}
