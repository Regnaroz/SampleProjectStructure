using Hangfire;

namespace SampleProject.Web.HangFireFiles;

public class RequeryingJobService : BackgroundService
{
    private readonly IRecurringJobManager _recurringJobManager;

    public RequeryingJobService(IRecurringJobManager recurringJobManager)
    {
        _recurringJobManager = recurringJobManager;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _recurringJobManager.AddOrUpdate<TaskService>("taskOne", s => s.SendEmail(), "*/15 * * * *");
        _recurringJobManager.AddOrUpdate<TaskService>("taskTwo", s => s.SendEmail(), "*/15 * * * *");
        return Task.CompletedTask;
    }
}
