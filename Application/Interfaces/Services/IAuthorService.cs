using Application.Models.Author;

namespace Application.Interfaces.Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
    Task<IEnumerable<AuthorDto>> GetAuthorsByIdsAsync(List<int> ids);
    Task<IEnumerable<AuthorDto>> GetAuthorsByNameAsync(string authorName);
    Task<AuthorDetailsDto> GetAuthorByIdAsync(int id);
    Task AddAuthorAsync(CreateAuthorDto authorDto);
    Task UpdateAuthorAsync(int id, UpdateAuthorDto authorDto);
    Task DeleteAuthorAsync(int id);
}
