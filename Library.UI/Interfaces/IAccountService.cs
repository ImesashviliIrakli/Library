using Library.UI.Models.Dtos;

namespace Library.UI.Interfaces;

public interface IAccountService
{
    public Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    public Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
}
