using Library.UI.Models.Dtos;

namespace Library.UI.Interfaces;

public interface IBaseService
{
    public Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer = true);
}
