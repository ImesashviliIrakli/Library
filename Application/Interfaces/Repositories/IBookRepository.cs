using Domain;

namespace Application.Interfaces.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book> GetByIdAsync(int id);
    Task<IEnumerable<Book>> GetBooksByTitleAsync(string title);
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int id);
    Task AddAuthorToBookAsync(Book book, Author author);
    Task RemoveAuthorFromBookAsync(Book book, Author author);
}
