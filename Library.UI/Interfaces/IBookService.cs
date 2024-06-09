using Library.UI.Models.BookDtos;
using Library.UI.Models.Dtos;

namespace Library.UI.Interfaces;

public interface IBookService
{
    Task<ResponseDto> GetAllBooksAsync();
    Task<ResponseDto> GetBooksByTitleAsync(string title);
    Task<ResponseDto> GetBookByIdAsync(int id);
    Task<ResponseDto> AddBookAsync(CreateBookDto createBookDto);
    Task<ResponseDto> UpdateBookAsync(int id, UpdateBookDto updateBookDto);
    Task<ResponseDto> DeleteBookAsync(int id);
    Task<ResponseDto> CheckoutBookAsync(int id);
    Task<ResponseDto> ReturnBookAsync(int id);
    Task<ResponseDto> AddAuthorToBookAsync(int bookId, int authorId);
    Task<ResponseDto> RemoveAuthorFromBookAsync(int bookId, int authorId);
}
