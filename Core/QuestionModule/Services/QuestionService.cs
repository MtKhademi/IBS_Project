using Common.Exceptions;
using Core.DAL;
using Core.QuestionModule.Abstractions.Dtos;
using Core.QuestionModule.Abstractions.Enumerations;
using Core.QuestionModule.Abstractions.Services;
using Core.QuestionModule.Infrastructure.ExcellFileHelper;
using Core.QuestionModule.MapperServices;
using Core.QuestionModule.ValidationServices;
using Core.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.QuestionModule.Services;

internal class QuestionService(
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

                await context.Questions.AddAsync(item);
                await context.SaveChangesAsync();
                //   await unitOfWork.QuestionRepository.CreateAsync(item);
                //  await unitOfWork.SaveChangeAsync();

                var xc = context.Questions.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception($"ERROR : {item}", ex);
            }

            var xx = unitOfWork.QuestionRepository.GetsQueryableTracker().ToList();

        }

        await unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<QuestionGetDto>> GetsByFilterAsync(QuestionGetFilterDto? filter)
    {
        validationService.IsValidAndThrowException(filter);

        var typeQuestion = filter.TypeOfQuestion.Value;
        var result = await unitOfWork.QuestionRepository.GetsQueryableNoTracker()
            .Where(x => x.TypeOfQuestion == typeQuestion).ToListAsync();


        var xx = unitOfWork.QuestionRepository.GetsQueryableTracker().ToList();
        var xc = context.Questions.ToList();


        return questionMapperService.Maps(result);

    }
}
