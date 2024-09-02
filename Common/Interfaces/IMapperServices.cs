namespace Common.Interfaces.MapperServices
{

    public interface IBaseMapperService { }


    public interface IMapperService<TModelSource, TModelDestination> : IBaseMapperService
        where TModelDestination : class
        where TModelSource : class
    {
        TModelDestination Map(TModelSource model);
    }
    public interface IMappersService<TModelSource, TModelDestination> : IBaseMapperService
        where TModelDestination : class
        where TModelSource : class
    {
        IEnumerable<TModelDestination> Maps(IEnumerable<TModelSource> models);
    }
    public interface IMappersIQueryService<TModelSource, TModelDestination> : IBaseMapperService
       where TModelDestination : class
       where TModelSource : class
    {
        Task<IEnumerable<TModelDestination>> MapsAsync(IQueryable<TModelSource> models);
    }
    public interface IMapperUpdateService<TModelSource, TModelDestination> : IBaseMapperService
        where TModelDestination : class
        where TModelSource : class
    {
        void MapUpdate(TModelSource source, TModelDestination destination);
    }

}
