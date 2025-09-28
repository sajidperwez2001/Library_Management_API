namespace Library.Api.Models;

public enum LoanStatus { Borrowed = 0, Returned = 1, Overdue = 2 }

public class Loan
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book? Book { get; set; }
    public int MemberId { get; set; }
    public Member? Member { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public LoanStatus Status { get; set; }
    public decimal Fine { get; set; } = 0m;
}
