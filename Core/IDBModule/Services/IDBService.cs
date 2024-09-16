using Common.Exceptions;
using Core.DAL;
using Core.IDBModule.Abstractions.Dtos;
using Core.IDBModule.Abstractions.Services;
using Core.IDBModule.Infrastructure.ExcellFileHelper;
using Core.IDBModule.MapperServices;
using Core.IDBModule.ValidationServices;
using Microsoft.EntityFrameworkCore;

namespace Core.IDBModule.Services;

public class IDBService(
    IUnitOfWork unitOfWork,
    IIDBValidationService validation,
    IIDBMapperService mapper) : IIDBService
{

    public async Task AddWithExcellFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
            throw new NotValidDataException($"This file is not exist : {filePath}");


        using var transaction = await unitOfWork.CreateTransactionAsync();

        await unitOfWork.IDBRepository.DeletesAllHardAsync();

        var excellFileManager = new ExcellFileManager();

        var listOfQuestions = excellFileManager.GetQuestions(filePath);
        foreach (var item in listOfQuestions)
        {
            try
            {
                await unitOfWork.IDBRepository.CreateAsync(item);
            }
            catch (Exception ex)
            {

                throw new Exception($"ERROR : {item}", ex);
            }
        }

        await unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<IDBGetDto>> GetsByFilterAsync(IDBGetFilterDto filter)
    {
        validation.IsValidAndThrowException(filter);

        var results = await unitOfWork.IDBRepository.GetsQueryableNoTracker()
            .Where(x => x.TypeOfIDB == filter.TypeOfIDB.Value)
            .ToListAsync();

        return mapper.Maps(results);
    }
}
