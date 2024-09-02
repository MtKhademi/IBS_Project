using Hangfire.Dashboard;

namespace Api.Commons.Attributes
{
    public class HangfierDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext dashboardContext)
        {
            return true;
        }
    }
}