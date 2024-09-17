using Common.Exceptions;
using Core.SymptomsModule.Abstractions.Dtos;

namespace Core.SymptomsModule.ValidationServices
{
    internal class SymptomValidationService : ISymptomValidationService
    {
        public void IsValidAndThrowException(SymptomAddOrUpdateDto model)
        {
            if (model is null)
            {
                throw new NotValidDataException("Please enter filter");
            }

            var isValid = new SymptomAddOrUpdateDtoValidator().Validate(model);
            if (!isValid.IsValid)
                throw new NotValidDataException(isValid.Errors);
        }
    }
}
