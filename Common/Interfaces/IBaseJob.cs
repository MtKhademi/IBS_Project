namespace Common.Interfaces;

public interface IBaseJob
{
    Task ExecuteAsync();
}

//public interface IJob<TResult>
//{
//    Task<TResult> ExecuteAsync();
//}
