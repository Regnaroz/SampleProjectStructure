using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Authentication.Common;
using SampleProject.Application.Authentication.Register.Commands.RegisterUser;
using ErrorOr;
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
    public async Task<IActionResult> RegisterUser(RegisterUserDto userDto)
    {
        var command = _mapper.Map<RegisterUserCommand>(userDto);

        var result = await _sender.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<AuthenticationResponseDto>(result)),
            errors => Problem(errors));
    }
}
