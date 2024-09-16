using Common.Exceptions;
using Core.IDBModule.Abstractions.Dtos;

namespace Core.IDBModule.ValidationServices
{
    internal class IDBValidationService : IIDBValidationService
    {
        public void IsValidAndThrowException(IDBGetFilterDto model)
        {
            if ((model is null))
            {
                throw new NotValidDataException("Please enter filter"); 
            }

            var isValid  = (new IDBGetFilterDtoValidator()).Validate(model);
            if (!isValid.IsValid)
                throw new NotValidDataException(isValid.Errors);
        }
    }
}
