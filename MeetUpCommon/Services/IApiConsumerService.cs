using MeetUpCommon.Models.Service;

namespace MeetUpCommon.Services;

public interface IApiConsumerService
{
    Task<ResponseModel> PostAsync(RequestModel request);
    Task<ResponseModel> GetAsync(RequestModel request, string urlParameter);
}