//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common;
using Common.Interfaces;
using Core.QuestionModule.Abstractions.Enumerations;
using Core.QuestionModule.Entities;

namespace Core.QuestionModule.Repositories;

public interface IQuestionRepository :
    ICRUDRepository<int,QuestionEntity>,
    IDeleteHardRepository<ETypeOfQuestion>
{
}
