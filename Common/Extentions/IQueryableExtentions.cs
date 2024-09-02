using Common.Infrastructure;

namespace MrMohande3Project.Common.Extentions
{
    public static class IQueryableExtentions
    {

        public static IBasePagination InitializePagination(this IBasePagination pagination)
        {
            if (!pagination.CurrentPage.HasValue) pagination.CurrentPage = 0;
            if (pagination.CurrentPage.Value < 0) pagination.CurrentPage = 0;
            if (!pagination.SizeOfPage.HasValue) pagination.SizeOfPage = 100;
            return pagination;
        }

        public static IQueryable<TResult> SetPagination<TResult>(this IQueryable<TResult> value, int currentPage = 1, int sizePage = 20)
        {
            return value.Skip((currentPage) * sizePage).Take(sizePage);
        }

        public static IQueryable<TResult> SetPagination<TResult>(this IQueryable<TResult> value, IBasePagination pagination)
        {

            pagination.InitializePagination();

            if (pagination.CurrentPage.HasValue && pagination.SizeOfPage.HasValue)
                return value.Skip((pagination.CurrentPage.Value) * pagination.SizeOfPage.Value).Take(pagination.SizeOfPage.Value);
            return value;
        }

    }
}
