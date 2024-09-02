namespace Common.Interfaces
{
    public interface IBaseService { }

    #region Genereic CRUD Services
    public interface IBaseService<TKeyModel, TModel, TFilterModel, TAddModel, TUpdateModel, TTableModel, TFilterTableModel> :
        IBaseService,
        IGetCountServiceAsync<TFilterModel>,
        IGetByIdServiceAsync<TKeyModel, TModel>,
        IGetsByFilterServiceAsync<TModel, TFilterModel>,
        IGetTableServiceAsync<TTableModel, TFilterTableModel>,
        IGetAllServiceAsync<TModel>,

        IAddServiceAsync<TModel, TAddModel>,

        IUpdateServiceAsync<TModel, TUpdateModel>,

        IDeleteSoftServiceAsync<TKeyModel>,
        IDeleteHardServiceAsync<TKeyModel>

    {
    }

    #endregion

    #region Get Services 

    public interface IGetByIdServiceAsync<TKeyModel, TModel> : IBaseService
    {
        Task<TModel?> GetByIdAsync(TKeyModel id);
    }
    public interface IGetsByFilterServiceAsync<TModel, TFilterModel> : IBaseService
    {
        Task<IEnumerable<TModel>> GetsByFilterAsync(TFilterModel filter);
    }
    public interface IGetsByFilterTableServiceAsync<TModel, TFilterModel> : IBaseService
    {
        Task<IEnumerable<TModel>> GetsByFilterTableAsync(TFilterModel filter);
    }
    public interface IGetSingleByFilterServiceAsync<TModel, TFilterModel> : IBaseService
    {
        Task<TModel> GetSingleByFilterAsync(TFilterModel filter);
    }
    public interface IGetAllServiceAsync<TModel> : IBaseService

    {
        Task<IEnumerable<TModel>> GetAllAsync();
    }
    public interface IGetTableServiceAsync<TTableModel, TFilterTableModel> : IBaseService
    {
        Task<TTableModel> GetTableAsync(TFilterTableModel filterTable);
    }
    public interface IGetCountServiceAsync<TFilterModel> :
        IBaseService

    {
        Task<int> GetCountAsync(TFilterModel filter);
    }
    public interface IGetCountLongServiceAsync<TFilterModel> :
        IBaseService

    {
        Task<long> GetCountLongAsync(TFilterModel filter);
    }


    #endregion

    #region Add Services

    public interface IAddServiceAsync<TAddModel> : IBaseService
    {
        Task AddAsync(TAddModel addModel);
    }
    public interface IAddServiceAsync<TModel, TAddModel> : IBaseService
    {
        Task<TModel> AddAsync(TAddModel addModel);
    }

    #endregion

    #region Update Services

    public interface IUpdateServiceAsync<TUpdateModel> : IBaseService
    {
        Task UpdateAsync(TUpdateModel updateDto);
    }
    public interface IUpdateServiceAsync<TModel, TUpdateModel> : IBaseService
    {
        Task<TModel> UpdateAsync(TUpdateModel updateDto);
    }

    #endregion

    #region Add Or Update Services

    public interface IAddOrUpdateServiceAsync<TAddModel> : IBaseService
    {
        Task AddOrUpdateAsync(TAddModel addModel);
    }

    public interface IAddOrUpdateServiceAsync<TModel, TAddModel> : IBaseService
    {
        Task<TModel> AddOrUpdateAsync(TAddModel dto);
    }

    #endregion

    #region Delete services

    public interface IDeleteSoftServiceAsync<TKeyModel> : IBaseService
    {
        Task DeleteSoftAsync(TKeyModel id);
    }
    public interface IDeleteAllSoftServiceAsync : IBaseService
    {
        Task DeleteAllSoftAsync();
    }

    public interface IDeleteHardServiceAsync<TKeyModel> : IBaseService
    {
        Task DeleteHardAsync(TKeyModel id);
    }

    public interface ITruncateServiceAsync : IBaseService
    {
        Task TruncateAsync();
    }


    #endregion

}