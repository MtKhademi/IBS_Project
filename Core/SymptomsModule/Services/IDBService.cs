using Common.Exceptions;
using Core.DAL;
using Core.SymptomsModule.Abstractions.Dtos;
using Core.SymptomsModule.Abstractions.Services;
using Core.SymptomsModule.Entities;
using Core.SymptomsModule.MapperServices;
using Core.SymptomsModule.ValidationServices;
using Core.UserManagement.Abstractions.Exceptions;
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


            //entity.DateTimeOfUpdate = DateTime.Now;
            //entity.Value = addModel.Value.Value;
            //await unitOfWork.SaveChangeAsync();
        }
        // if not exist lastItemSpecial => add with lastItemTital dateTime
        // if exist lastItemSpecial
        // if in the same week => then update
        // if not the same week => create new 

        //.Where(x => x.TypeOfSymptom == addModel.TypeOfSymptom)
        //.Where(x => x.DateTimeOfCreation.Date == DateTime.Now.Date)
        //.SingleOrDefaultAsync();



    }

    public async Task<IEnumerable<SymptomEntity>> GetAllAsync()
        => await unitOfWork.SymptomRepository.GetsQueryableNoTracker().ToListAsync();
}
