using Common.Exceptions;
using Core.DAL;
using Core.QuestionModule.Abstractions.Dtos;
using Core.QuestionModule.Abstractions.Dtos.QuestionAnswerDtos;
using Core.QuestionModule.Abstractions.Enumerations;
using Core.QuestionModule.Abstractions.Exceptions;
using Core.QuestionModule.Abstractions.Services;
using Core.QuestionModule.Infrastructure.ExcellFileHelper;
using Core.QuestionModule.MapperServices;
using Core.QuestionModule.ValidationServices;
using Core.UserManagement.Abstractions.Exceptions;
using Core.UserManagement.Abstractions.Services;
using Core.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.QuestionModule.Services;

internal class QuestionService(
    IUserService userService,
    IQuestionValidationService validationService,
    IQuestionMapperService questionMapperService,
    IUnitOfWork unitOfWork,
    DatabaseContext context) : IQuestionService
{
    public async Task AddWithExcellFileAsync(UserManagementModel userManagementModel, string filePath, ETypeOfQuestion typeOfQuestion)
    {
        if (!File.Exists(filePath))
            throw new NotValidDataException($"This file is not exist : {filePath}");


        using var transaction = await unitOfWork.CreateTransactionAsync();

        await unitOfWork.QuestionRepository.DeleteHardAsync(typeOfQuestion);

        var excellFileManager = new ExcellFileManager();

        var listOfQuestions = excellFileManager.GetQuestions(filePath, typeOfQuestion);
        foreach (var item in listOfQuestions)
        {
            try
            {

                await unitOfWork.QuestionRepository.CreateAsync(item);
            }
            catch (Exception ex)
            {

                throw new Exception($"ERROR : {item}", ex);
            }
        }

        await unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<QuestionGetDto>> GetsByFilterAsync(QuestionGetFilterDto? filter)
    {
        validationService.IsValidAndThrowException(filter);

        var typeQuestion = filter.TypeOfQuestion.Value;
        var result = await unitOfWork.QuestionRepository.GetsQueryableNoTracker()
            .Where(x => x.TypeOfQuestion == typeQuestion).ToListAsync();


        return questionMapperService.Maps(result);

    }

    public async Task QuestionAnswerSetAsync(QuestionAnswerSetDto? answer)
    {
        validationService.IsValidAndThrowException(answer);

        var user = await userService.GetByUserNameAsync(answer.UserName);
        if (user is null)
            throw new UserNameNotExistException(answer.UserName);

        var question = await unitOfWork.QuestionRepository.GetByIDAsync(answer.QuestionId.Value);
        if (question is null)
            throw new QuestionNotExistWithIdException(answer.QuestionId.Value);

        var answerEntity = await unitOfWork.QuestionAnswerRepository.GetsQueryableTracker()
            .SingleOrDefaultAsync(x => x.UserId == user.Id && x.QuestionId == question.Id);

        if (answerEntity is not null)
            await unitOfWork.QuestionAnswerRepository.DeleteHardAsync(answerEntity);

        await unitOfWork.QuestionAnswerRepository.CreateAsync(
            new Entities.QuestionAnswerEntity
            {
                Degree = answer.Degree.Value,
                User = user,
                UserId = user.Id,
                QuestionId = question.Id,
                Question = question
            });
        await unitOfWork.SaveChangeAsync();

    }
}
