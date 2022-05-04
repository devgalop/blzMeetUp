namespace MeetUpBack.Helpers;

public interface IMappingHelper
{
    OutputModel ConvertTo<OutputModel,InputModel>(InputModel model);
}