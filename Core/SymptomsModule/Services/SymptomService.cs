using Common.Exceptions;
using Common.Extentions;
using Core.DAL;
using Core.SymptomsModule.Abstractions.Dtos;
using Core.SymptomsModule.Abstractions.Enums;
using Core.SymptomsModule.Abstractions.Services;
using Core.SymptomsModule.Entities;
using Core.SymptomsModule.MapperServices;
using Core.SymptomsModule.ValidationServices;
using Core.UserManagement.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.SymptomsModule.Services;

public class SymptomService(
    IUnitOfWork unitOfWork,
    ISymptomValidationService validation,
    ISymptomMapperService mapper) : ISymptomService
{
    public async Task AddOrUpdateAsync(SymptomAddOrUpdateDto addModel)
    {
        validation.IsValidAndThrowException(addModel);


        var user = await unitOfWork.UserRepository.GetsQueryableNoTracker()
            .SingleOrDefaultAsync(x => x.UserName == addModel.UserName);
        if (user == null)
            throw new UserNameNotExistException(addModel.UserName);

        var entities = await unitOfWork.SymptomRepository
            .GetsQueryableTracker()
            .Where(x => x.UserId == user.Id)
            .OrderByDescending(x => x.DateTimeOfCreation)
            .ToListAsync();


        var lastItemTotal = entities.FirstOrDefault();
        var lastItemSpecial = entities.FirstOrDefault(x => x.TypeOfSymptom == addModel.TypeOfSymptom);


        if (lastItemTotal is null)
        {
            var entity = mapper.Map(addModel);
            entity.UserId = user.Id;
            await unitOfWork.SymptomRepository.CreateAsync(entity);
            await unitOfWork.SaveChangeAsync();
        }
        else
        {
            if (lastItemSpecial is null)
            {
                var entity = mapper.Map(addModel);
                entity.UserId = user.Id;
                entity.DateTimeOfUpdate = lastItemTotal.DateTimeOfUpdate;
                await unitOfWork.SymptomRepository.CreateAsync(entity);
                await unitOfWork.SaveChangeAsync();
            }
            else
            {
                // بیشتر باشه میشه هفته جدید
                if ((DateTime.Now.Date - lastItemSpecial.DateTimeOfUpdate.Date).Days >= 7)
                {
                    var entity = mapper.Map(addModel);
                    entity.UserId = user.Id;
                    entity.DateTimeOfUpdate = lastItemSpecial.DateTimeOfUpdate.Date.AddDays(7);
                    await unitOfWork.SymptomRepository.CreateAsync(entity);
                    await unitOfWork.SaveChangeAsync();
                }
                // در غیر اینصورت باید آپدیت بشه همین
                else
                {
                    lastItemSpecial.Value = addModel.Value.Value;
                    await unitOfWork.SaveChangeAsync();
                }
            }

        }
    }

    public async Task<IEnumerable<SymptomEntity>> GetAllAsync()
        => await unitOfWork.SymptomRepository.GetsQueryableNoTracker().ToListAsync();

    public async Task<List<SymptomChartDataDto>> GetChartAsync(string? userName)
    {

        if (string.IsNullOrWhiteSpace(userName))
            throw new NotValidDataException("Please enter userName");

        var user = await unitOfWork.UserRepository.GetsQueryableNoTracker()
            .SingleOrDefaultAsync(x => x.UserName == userName);

        if (user is null)
            throw new UserNameNotExistException(userName);

        var result = new List<SymptomChartDataDto>();

        var types = EnumExtensions.ToDictionaryWithNameAndType<ETypeOfSymptoms>();
        foreach (var typeOfSymptoms in types)
        {
            result.Add(new SymptomChartDataDto
            {
                TypeOfSymptoms = typeOfSymptoms.Value,
                Spots = await GetSpotsAsync(user.Id, typeOfSymptoms.Value)
            });
        }

        return result;
    }

    private async Task<List<SymptomChartDataSpotDto>> GetSpotsAsync(int userId, ETypeOfSymptoms typeOfSymptom)
    {
        var result = new List<SymptomChartDataSpotDto>();

        var data = await unitOfWork.SymptomRepository
            .GetsQueryableNoTracker()
            .Where(x => x.UserId == userId)
            .Where(x => x.TypeOfSymptom == typeOfSymptom).ToListAsync();

        int week = 1;
        foreach (var spot in data)
        {
            result.Add(new SymptomChartDataSpotDto
            {
                Week = week++,
                Degree = spot.Value
            });
        }

        return result;
    }
}
