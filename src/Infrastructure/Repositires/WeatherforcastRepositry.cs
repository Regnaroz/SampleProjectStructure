using SampleProject.Application.IRepositries;
using SampleProject.Domain.Entities;

namespace SampleProject.Infrastructure.Repositires;
public class WeatherforcastRepositry : IWeatherforcastRepositry
{
    private static readonly string[] Summaries = new[]
   {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    public async Task<IEnumerable<WeatherForecast>> GetWeatherData()
    {
        await Task.CompletedTask;
        var rng = new Random();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        });
    }
}
