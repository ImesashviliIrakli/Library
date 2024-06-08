using Library.UI.Interfaces;
using Library.UI.Models.Dtos;
using Library.UI.Utility;

namespace Library.UI.Services;

public class AccountService : IAccountService
{

    private readonly IBaseService _baseService;
    public AccountService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = loginRequestDto,
            Url = SD.LibraryAPIBase + "/api/Auth/login"
        }, withBearer: false);
    }

    public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = registrationRequestDto,
            Url = SD.LibraryAPIBase + "/api/Auth/register"
        }, withBearer: false);
    }
}