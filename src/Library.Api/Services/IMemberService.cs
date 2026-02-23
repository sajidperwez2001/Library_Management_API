using Library.Api.DTOs;

namespace Library.Api.Services;

public interface IMemberService
{
    Task<IEnumerable<MemberReadDto>> GetAllAsync();
    Task<MemberReadDto?> GetByIdAsync(int id);
    Task<MemberReadDto> CreateAsync(MemberCreateDto dto);
    Task<bool> UpdateAsync(int id, MemberCreateDto dto);
    Task<bool> DeleteAsync(int id);
}