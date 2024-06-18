namespace SampleProject.Web.HangFireFiles;

public class TaskService
{
    public async Task<bool> SendEmail()
    {
        return await Task.FromResult(true);
    }
}
