using Common.Exceptions;
using Core.DAL;
using Core.SymptomsModule.Abstractions.Dtos;
using Core.SymptomsModule.Abstractions.Services;
using Core.SymptomsModule.Entities;
using Core.SymptomsModule.MapperServices;
using Core.SymptomsModule.ValidationServices;
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

        var entity = await unitOfWork.SymptomRepository.GetsQueryableTracker()
            .Where(x => x.TypeOfSymptom == addModel.TypeOfSymptom)
            .Where(x => x.DateTimeOfCreation.Date == DateTime.Now.Date)
            .SingleOrDefaultAsync();

        if (entity is null)
        {
            entity = mapper.Map(addModel);
            await unitOfWork.SymptomRepository.CreateAsync(entity);
            await unitOfWork.SaveChangeAsync();
        }
        else
        {
            entity.DateTimeOfUpdate = DateTime.Now;
            entity.Value = addModel.Value.Value;
            await unitOfWork.SaveChangeAsync();
        }

    }

    public async Task<IEnumerable<SymptomEntity>> GetAllAsync()
        => await unitOfWork.SymptomRepository.GetsQueryableNoTracker().ToListAsync();
}
