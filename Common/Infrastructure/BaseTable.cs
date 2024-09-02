//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

namespace Common.Infrastructure
{

    public class BaseTable : IBasePagination, IPagination
    {
        public int? CurrentPage { get; set; } = 1;
        public int? SizeOfPage { get; set; } = 50;
        public int? CountOfAllLogs { get; set; }
        public int? CountOfAllLogsBaseFilter { get; set; }
    }
    public class BaseTable<TResult> : BaseTable
    {

        public BaseTable()
        {

        }
        public BaseTable(IBasePagination pagination)
        {
            this.CurrentPage = pagination.CurrentPage;
            this.SizeOfPage = pagination.SizeOfPage;
        }
        public int? CountOfAllPages
        {
            get
            {

                if (Results == null)
                    return 0;
                if (CountOfAllLogsBaseFilter == null)
                    return 0;
                if (SizeOfPage == null)
                    return 0;

                int count1 = (int)(CountOfAllLogsBaseFilter / SizeOfPage);
                double d1 = (double)CountOfAllLogsBaseFilter / (double)SizeOfPage;
                if (d1 == count1)
                    return count1;
                return count1 + 1;
            }
        }


        public IEnumerable<TResult>? Results { get; set; } = default;
    }
    public interface IBasePagination
    {
        int? CurrentPage { get; set; }
        int? SizeOfPage { get; set; }
    }
    public interface IPagination
    {
        int? CountOfAllLogsBaseFilter { get; }
        int? CountOfAllLogs { get; set; }
    }
}
