//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common.Interfaces.MapperServices;
using Core.QuestionModule.Abstractions.Dtos;
using Core.QuestionModule.Entities;

namespace Core.QuestionModule.MapperServices;

internal interface IQuestionMapperService :
    IMapperService<QuestionEntity, QuestionGetDto>,
    IMappersService<QuestionEntity, QuestionGetDto>
{
}
