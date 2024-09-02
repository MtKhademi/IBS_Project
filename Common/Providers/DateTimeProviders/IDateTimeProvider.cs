using Common.Interfaces;

namespace Common.Providers.DateTimeProviders
{
    public interface IDateTimeProvider : IBaseService
    {
        DateTime Now { get; }
    }
}
