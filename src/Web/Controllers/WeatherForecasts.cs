using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace SampleProject.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WeatherForecasts : ApiController
{
    private readonly ISender sender;

    public WeatherForecasts(ISender sender)
    {
        this.sender = sender;
    }

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
    {
        return await sender.Send(new GetWeatherForecastsQuery());
    }
}
