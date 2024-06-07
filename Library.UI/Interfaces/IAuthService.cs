using Library.UI.Models.Dtos;

namespace Library.UI.Interfaces;

public interface IAuthService
{
    public Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    public Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
}
