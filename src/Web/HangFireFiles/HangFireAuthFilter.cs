using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace SampleProject.Web.HangFireFiles;

public class HangFireAuthFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();

        //if(false)//Auth check here ex check for roles isAdmin
        //     return false;

        return true;
    }
}
