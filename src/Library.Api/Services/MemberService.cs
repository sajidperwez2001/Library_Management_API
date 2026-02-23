using Library.Api.Data;
using Library.Api.DTOs;
using Library.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Services;

public class MemberService : IMemberService
{
    private readonly LibraryContext _db;
    public MemberService(LibraryContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<MemberReadDto>> GetAllAsync()
    {
        return await _db.Members
            .Select(m => new MemberReadDto(
                m.Id,
                m.FullName,
                m.Email,
                m.Phone,
                m.Address,
                m.MembershipDate,
                m.IsActive
            ))
            .ToListAsync();
    }

    public async Task<MemberReadDto?> GetByIdAsync(int id)
    {
        var m = await _db.Members.FindAsync(id);
        if (m == null) return null;

        return new MemberReadDto(
            m.Id,
            m.FullName,
            m.Email,
            m.Phone,
            m.Address,
            m.MembershipDate,
            m.IsActive
        );
    }

    public async Task<MemberReadDto> CreateAsync(MemberCreateDto dto)
    {
        var member = new Member
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address,
            MembershipDate = DateTime.UtcNow,
            IsActive = dto.IsActive
        };

        _db.Members.Add(member);
        await _db.SaveChangesAsync();

        return new MemberReadDto(
            member.Id,
            member.FullName,
            member.Email,
            member.Phone,
            member.Address,
            member.MembershipDate,
            member.IsActive
        );
    }

    public async Task<bool> UpdateAsync(int id, MemberCreateDto dto)
    {
        var member = await _db.Members.FindAsync(id);
        if (member == null) return false;

        member.FullName = dto.FullName;
        member.Email = dto.Email;
        member.Phone = dto.Phone;
        member.Address = dto.Address;
        member.IsActive = dto.IsActive;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var member = await _db.Members.FindAsync(id);
        if (member == null) return false;

        _db.Members.Remove(member);
        await _db.SaveChangesAsync();
        return true;
    }
}