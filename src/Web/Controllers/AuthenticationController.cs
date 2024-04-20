using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Authentication.Common;
using SampleProject.Application.Authentication.Register.Commands.RegisterUser;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Entities;
using SampleProject.Application.Authentication.Login.Commands.LoginUser;
namespace SampleProject.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<AuthenticationResponseDto>> RegisterUser(RegisterUserDto userDto)
    {
        var command = _mapper.Map<RegisterUserCommand>(userDto);

        var result = await _sender.Send(command);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }
    [HttpPost("Login")]
    public async Task<ActionResult<AuthenticationResponseDto>> Login(LoginUserDto userDto)
    {
        var command = _mapper.Map<LoginUserCommand>(userDto);

        var result = await _sender.Send(command);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }
}
