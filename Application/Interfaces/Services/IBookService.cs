using Application.Models.Author;
using Application.Models.Book;

namespace Application.Interfaces.Services;

public interface IBookService
{
    Task<IEnumerable<BookListDto>> GetAllBooksAsync();
    Task<BookDetailsDto> GetBookByIdAsync(int id);
    Task AddBookAsync(CreateBookDto bookDto);
    Task UpdateBookAsync(int id, UpdateBookDto bookDto);
    Task DeleteBookAsync(int id);
    Task CheckoutBookAsync(int id);
    Task ReturnBookAsync(int id);
}
