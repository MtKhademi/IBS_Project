//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using AutoMapper;
using Core.QuestionModule.Abstractions.Dtos;
using Core.QuestionModule.Entities;

namespace Core.QuestionModule.MapperServices;

internal class QuestionMapperService(
    IMapper mapper) : IQuestionMapperService
{
    public QuestionGetDto Map(QuestionEntity model) => mapper.Map<QuestionGetDto>(model);
    public IEnumerable<QuestionGetDto> Maps(IEnumerable<QuestionEntity> models) => mapper.Map<IEnumerable<QuestionGetDto>>(models);
}
