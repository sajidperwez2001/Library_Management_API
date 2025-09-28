using Microsoft.AspNetCore.Mvc;
using Library.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _service;
    public LoansController(ILoanService service) { _service = service; }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [Authorize]
    [HttpPost("borrow")]
    public async Task<IActionResult> Borrow([FromBody] BorrowRequest req)
    {
        var loan = await _service.BorrowAsync(req.BookId, req.MemberId, req.Days ?? 14);
        if (loan == null) return BadRequest(new { message = "Cannot borrow" });
        return Ok(loan);
    }

    [Authorize]
    [HttpPost("{id:int}/return")]
    public async Task<IActionResult> Return(int id)
    {
        var loan = await _service.ReturnAsync(id);
        if (loan == null) return BadRequest(new { message = "Cannot return" });
        return Ok(loan);
    }
}

public record BorrowRequest(int BookId, int MemberId, int? Days);
