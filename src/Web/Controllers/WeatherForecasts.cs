using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using SampleProject.Domain.Constants;

namespace SampleProject.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.User)]
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
