using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Authentication.Register.Commands.RegisterUser;

namespace SampleProject.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;

    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<string>> RegisterUser(RegisterUserDto userDto)
    {
        var command = new RegisterUserCommand(userDto.firstName, userDto.lastName,userDto.password,userDto.email);
        var result = await _sender.Send(command);
        return Ok(result);
    }
}
