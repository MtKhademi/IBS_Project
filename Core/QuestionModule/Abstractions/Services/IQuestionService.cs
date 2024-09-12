//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common.Interfaces;
using Core.QuestionModule.Abstractions.Dtos;
using Core.QuestionModule.Abstractions.Dtos.QuestionAnswerDtos;
using Core.QuestionModule.Abstractions.Enumerations;
using Core.UserManagement.Models;

namespace Core.QuestionModule.Abstractions.Services;

public interface IQuestionService : IGetsByFilterServiceAsync<QuestionGetDto, QuestionGetFilterDto>
{
    Task AddWithExcellFileAsync(UserManagementModel userManagementModel, string pathToSave, ETypeOfQuestion typeOfQuestion);
    Task QuestionAnswerSetAsync(QuestionAnswerSetDto? answer);
}
