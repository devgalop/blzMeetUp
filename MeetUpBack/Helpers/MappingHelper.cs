using AutoMapper;

namespace MeetUpBack.Helpers;

public class MappingHelper : IMappingHelper
{
    private readonly IMapper _mapper;

    public MappingHelper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public OutputModel ConvertTo<OutputModel,InputModel>(InputModel model){
        var modelConverted = _mapper.Map<OutputModel>(model);
        return modelConverted;
    }
}