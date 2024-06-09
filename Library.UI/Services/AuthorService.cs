using Library.UI.Interfaces;
using Library.UI.Models.AuthorDtos;
using Library.UI.Models.Dtos;
using Library.UI.Utility;

namespace Library.UI.Services;

public class AuthorService : IAuthorService
{
    private readonly IBaseService _baseService;

    public AuthorService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> GetAuthorsAsync()
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = SD.LibraryAPIBase + "/api/Authors"
        });
    }

    public async Task<ResponseDto> GetAuthorsByNameAsync(string authorName)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = SD.LibraryAPIBase + "/api/Authors/" + authorName
        });
    }

    public async Task<ResponseDto> GetAuthorByIdAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = SD.LibraryAPIBase + $"/api/Authors/{id}"
        });
    }

    public async Task<ResponseDto> GetAuthorsByIdsAsync(List<int> ids)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = ids,
            Url = SD.LibraryAPIBase + $"/api/Authors/GetAuthorsByIds"
        });
    }

    public async Task<ResponseDto> AddAuthorAsync(CreateAuthorDto authorDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = authorDto,
            Url = SD.LibraryAPIBase + "/api/Authors"
        });
    }

    public async Task<ResponseDto> UpdateAuthorAsync(int id, UpdateAuthorDto authorDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.PUT,
            Data = authorDto,
            Url = SD.LibraryAPIBase + $"/api/Authors/{id}"
        });
    }

    public async Task<ResponseDto> DeleteAuthorAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.DELETE,
            Url = SD.LibraryAPIBase + $"/api/Authors/{id}"
        });
    }
}

