namespace Common.Interfaces;

public interface IBaseValidationInputService { }
public interface IValidationInputService<TModel> : IBaseValidationInputService
{
    void IsValidAndThrowException(TModel model);
}
