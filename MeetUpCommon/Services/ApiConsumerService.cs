using System.Net.Http.Headers;
using System.Text;
using MeetUpCommon.Models.Service;

namespace MeetUpCommon.Services;

public class ApiConsumerService : IApiConsumerService
{
    private HttpClient ConfigureClient(RequestModel request)
    {
        HttpClient client = new HttpClient
        {
            BaseAddress = new Uri(request.UrlBase)
        };

        if (request.HasAuthorization)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(request.TokenType, request.AccessToken);
        }
        return client;
    }

    public async Task<ResponseModel> PostAsync(RequestModel request)
    {
        try
        {
            HttpClient client = ConfigureClient(request);
            StringContent content = new StringContent(request.Body, Encoding.UTF8, "application/json");
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
        catch (Exception ex)
        {
            return new ResponseModel()
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ResponseModel> PutAsync(RequestModel request)
    {
        try
        {
            HttpClient client = ConfigureClient(request);
            StringContent content = new StringContent(request.Body, Encoding.UTF8, "application/json");
            string url = $"{request.ServicePrefix}{request.Controller}";
            HttpResponseMessage response = await client.PutAsync(url, content);
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
        catch (Exception ex)
        {
            return new ResponseModel()
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ResponseModel> GetAsync(RequestModel request, string urlParameter)
    {
        try
        {
            HttpClient client = ConfigureClient(request);
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
        catch (Exception ex)
        {
            return new ResponseModel()
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ResponseModel> DeleteAsync(RequestModel request, string urlParameter)
    {
        try
        {
            HttpClient client = ConfigureClient(request);
            string url = $"{request.ServicePrefix}{request.Controller}/{urlParameter}";
            HttpResponseMessage response = await client.DeleteAsync(url);
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
        catch (Exception ex)
        {
            return new ResponseModel()
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }
}