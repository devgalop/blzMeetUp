using System.Net.Http.Headers;
using System.Text;
using MeetUpCommon.Models.Service;

namespace MeetUpCommon.Services;

public class ApiConsumerService : IApiConsumerService
{
    public ApiConsumerService()
    {

    }

    public async Task<ResponseModel> PostAsync(RequestModel request)
    {
        StringContent content = new StringContent(request.Body, Encoding.UTF8, "application/json");
        HttpClient client = new HttpClient
        {
            BaseAddress = new Uri(request.UrlBase)
        };

        if (request.HasAuthorization)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(request.TokenType, request.AccessToken);
        }
        string url = $"{request.ServicePrefix}{request.Controller}";
        HttpResponseMessage response = await client.PostAsync(url, content);
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return new ResponseModel()
            {
                IsSuccess = false,
                Message = result
            };
        }

        return new ResponseModel()
        {
            IsSuccess = true,
            Result = result
        };
    }

    public async Task<ResponseModel> GetAsync(RequestModel request, string urlParameter)
    {
        HttpClient client = new HttpClient
        {
            BaseAddress = new Uri(request.UrlBase)
        };

        if (request.HasAuthorization)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(request.TokenType, request.AccessToken);
        }
        string url = $"{request.ServicePrefix}{request.Controller}/{urlParameter}";
        HttpResponseMessage response = await client.GetAsync(url);
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return new ResponseModel()
            {
                IsSuccess = false,
                Message = result
            };
        }

        return new ResponseModel()
        {
            IsSuccess = true,
            Result = result
        };
    }
}