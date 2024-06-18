using Hangfire;
using SampleProject.Web.HangFireFiles;

namespace SampleProject.Web;

public static class HangFireSettingBuilderRegister
{
    [Obsolete]
    public static void AddHangFire(this WebApplication? app)
    {
        app.UseHangfireDashboard(
                         default,
                         new DashboardOptions() { Authorization = new[] { new HangFireAuthFilter() } }
                         );

        app.UseHangfireServer();
    }
}
