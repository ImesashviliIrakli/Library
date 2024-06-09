using Library.UI.Interfaces;
using Library.UI.Models.BookDtos;
using Library.UI.Models.Dtos;
using Library.UI.Utility;

namespace Library.UI.Services;

public class BookService : IBookService
{
    private readonly IBaseService _baseService;
    public BookService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> GetAllBooksAsync()
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = SD.LibraryAPIBase + "/api/Books/"
        });
    }

    public async Task<ResponseDto> GetBooksByTitleAsync(string title)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = SD.LibraryAPIBase + "/api/Books/" + title
        });
    }

    public async Task<ResponseDto> GetBookByIdAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = SD.LibraryAPIBase + $"/api/Books/{id}"
        });
    }

    public async Task<ResponseDto> AddBookAsync(CreateBookDto createBookDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = createBookDto,
            Url = SD.LibraryAPIBase + "/api/Books"
        });
    }

    public async Task<ResponseDto> UpdateBookAsync(int id, UpdateBookDto updateBookDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.PUT,
            Data = updateBookDto,
            Url = SD.LibraryAPIBase + $"/api/Books/{id}"
        });
    }

    public async Task<ResponseDto> DeleteBookAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.DELETE,
            Url = SD.LibraryAPIBase + $"/api/Books/{id}"
        });
    }

    public async Task<ResponseDto> CheckoutBookAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Url = SD.LibraryAPIBase + $"/api/Books/{id}/checkout"
        });
    }

    public async Task<ResponseDto> ReturnBookAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Url = SD.LibraryAPIBase + $"/api/Books/{id}/return"
        });
    }

    public async Task<ResponseDto> AddAuthorToBookAsync(int bookId, int authorId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = new { bookId, authorId },
            Url = SD.LibraryAPIBase + $"/api/Books/AddAuthorToBook"
        });
    }

    public async Task<ResponseDto> RemoveAuthorFromBookAsync(int bookId, int authorId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = new { bookId, authorId },
            Url = SD.LibraryAPIBase + $"/api/Books/RemoveAuthorFromBook"
        });
    }
}
