using Domain;

namespace Application.Interfaces.Repositories;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<IEnumerable<Author>> GetByIdsAsync(List<int> ids);
    Task<IEnumerable<Author>> GetAuthorsByNameAsync(string authorName);
    Task<Author> GetByIdAsync(int id);
    Task AddAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(int id);
}
