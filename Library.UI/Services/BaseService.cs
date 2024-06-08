using Library.UI.Interfaces;
using Library.UI.Models.Dtos;
using Library.UI.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Library.UI.Services;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenProvider _tokenProvider;
    public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
    {
        _httpClientFactory = httpClientFactory;
        _tokenProvider = tokenProvider;
    }

    public async Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer = true)
    {
        try
        {
            HttpClient client = _httpClientFactory.CreateClient("LibraryAPI");
            HttpRequestMessage message = new();

            message.Headers.Add("Accept", "application/json");

            if (withBearer)
            {
                var token = _tokenProvider.GetToken();
                message.Headers.Add("Authorization", $"Bearer {token}");
            }

            message.RequestUri = new Uri(requestDto.Url);

            var temp = JsonConvert.SerializeObject(requestDto.Data, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            if (requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }),
                Encoding.UTF8, "application/json");
            }

            switch (requestDto.ApiType)
            {
                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            HttpResponseMessage apiResponse = await client.SendAsync(message);


            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new() { Status = 1, Message = "Not Found" };
                case HttpStatusCode.Forbidden:
                    return new() { Status = 1, Message = "Forbidden" };
                case HttpStatusCode.Unauthorized:
                    return new() { Status = 1, Message = "Unauthorized" };
                case HttpStatusCode.InternalServerError:
                    return new() { Status = 1, Message = "Internal Server Error" };
                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                    return apiResponseDto;
            }
        }
        catch (Exception ex)
        {
            var dto = new ResponseDto
            {
                Message = ex.Message,
                Status = 1
            };

            return dto;
        }
    }
}
