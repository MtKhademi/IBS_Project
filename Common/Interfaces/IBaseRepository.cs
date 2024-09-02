namespace Common.Interfaces
{

    public interface IBaseRepository { }

    public interface ICRUDRepository<TKeyType, TModel> :
        IBaseRepository,
        IGetsNoTrackingRepository<TModel>,
        IGetsTrackingRepository<TModel>,
        IGetByIdRepository<TKeyType, TModel>,

        ICreateRepository<TModel>,
        ICreatesRepository<TModel>,

        IUpdateRepository<TModel>,

        IDeleteHardRepository<TKeyType, TModel>,
        IDeletesHardByRepository<TKeyType, TModel>,
        ITruncateRepository,

        IDeletesSoftepository<TKeyType, TModel>,
        IDeleteSoftRepository<TKeyType, TModel>
        where TModel : class
    {

    }

    #region Get
    public interface IGetsNoTrackingRepository<TModel> : IBaseRepository
      where TModel : class
    {
        IQueryable<TModel> GetsQueryableNoTracker();
    }
    public interface IGetsTrackingRepository<TModel> : IBaseRepository
        where TModel : class
    {
        IQueryable<TModel> GetsQueryableTracker();
    }
    public interface IGetByIdRepository<TKeyType, TModel> : IBaseRepository
           where TModel : class
    {
        Task<TModel?> GetByIDAsync(TKeyType id);
    }

    #endregion

    #region Create Repository

    public interface ICreateRepository<TModel> : IBaseRepository
       where TModel : class
    {
        Task CreateAsync(TModel model);
    }
    public interface ICreatesRepository<TModel> : IBaseRepository
     where TModel : class
    {
        Task CreatesArrangeAsync(IEnumerable<TModel> models);
    }


    #endregion

    #region Update Repository

    public interface IUpdateRepository<TModel> : IBaseRepository
    where TModel : class
    {
        Task UpdateAsync(TModel model);
    }

    #endregion

    #region Delete

    public interface IDeleteHardRepository<TKeyModel, TModel> : IBaseRepository
       where TModel : class
    {
        Task DeleteHardAsync(TModel model);
        Task DeleteHardAsync(TKeyModel keyModel);
    }
    public interface IDeletesHardByRepository<TKeyModel, TModel> : IBaseRepository
       where TModel : class
    {
        Task DeletesHardByAsync(IEnumerable<TKeyModel> keys);
        Task DeletesHardByAsync(IEnumerable<TModel> models);
    }
    public interface ITruncateRepository : IBaseRepository
    {
        Task TruncateAsync();
    }


    public interface IDeleteSoftRepository<TKeyModel, TModel> : IBaseRepository
        where TModel : class
    {
        Task DeleteSoftAsync(TModel model);
        Task DeleteSoftAsync(TKeyModel keyModel);
    }
    public interface IDeletesSoftepository<TKeyModel, TModel> : IBaseRepository
       where TModel : class
    {
        Task DeletesSoftAsync(IEnumerable<TKeyModel> keys);
        Task DeletesSoftAsync(IEnumerable<TModel> models);
    }

  
    #endregion


}
