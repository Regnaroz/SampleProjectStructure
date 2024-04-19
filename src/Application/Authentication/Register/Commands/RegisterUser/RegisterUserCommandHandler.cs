using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Authentication.Common;
using SampleProject.Domain.Constants;
using SampleProject.Domain.Entities;
using SampleProject.Domain.Errors;
using static SampleProject.Application.Authentication.Common.AuthenticationResult;

namespace SampleProject.Application.Authentication.Register.Commands.RegisterUser;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<AuthenticationResult>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    async Task<ErrorOr<AuthenticationResult>> IRequestHandler<RegisterUserCommand, ErrorOr<AuthenticationResult>>.Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var isEmailExist = await _userManager.FindByEmailAsync(request.email);
        if (isEmailExist != null)
        {
            return Errors.Authentication.DuplicateEmail;
        }

        var user = new ApplicationUser()
        {
            Email = request.email,
            UserName = request.email,
        };

        var result = await _userManager.CreateAsync(user, request.password);
        if (!result.Succeeded)
        {
            return HandleRegiserationErrors(result);
        }

        user = await _userManager.FindByEmailAsync(request.email);
        if (user != null)
        {
            await _userManager.AddToRoleAsync(user!, Roles.Administrator);

            var userDto = new UserDto(user.Id, user.UserName!, user.UserName!, user.Email!);
            return new AuthenticationResult(userDto, string.Empty);
        }
        else
        {
            return Errors.Authentication.UserNotFound;
        }
    }

    private ErrorOr<AuthenticationResult> HandleRegiserationErrors(IdentityResult result)
    {
        var errors = new List<Error>();
        foreach (var error in result.Errors)
        {
            errors.Add(Error.Failure(code : error.Code , description: error.Description));
        }
        
        return errors;
    }
}
