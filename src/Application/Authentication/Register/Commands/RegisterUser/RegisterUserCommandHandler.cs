using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Authentication.Common;
using SampleProject.Domain.Constants;
using SampleProject.Domain.Entities;
using static SampleProject.Application.Authentication.Common.AuthenticationResult;

namespace SampleProject.Application.Authentication.Register.Commands.RegisterUser;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthenticationResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<AuthenticationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var isEmailExist = await _userManager.FindByEmailAsync(request.Email);
        if(isEmailExist != null)
        {
            throw new Exception("UserExist");
        }

        var user  = new ApplicationUser() {
            Email = request.Email,
            UserName = request.Email,
        };

        var result = await _userManager.CreateAsync(user,request.Password);
        if (!result.Succeeded) 
        { 
            throw new Exception(result.Errors.First().Code);
        }

        user = await _userManager.FindByEmailAsync(request.Email);
       if(user != null)
        {
            await _userManager.AddToRoleAsync(user!, Roles.Administrator);

            var userDto = new UserDto(user.Id, user.UserName!, user.UserName!, user.Email!);
            return new(userDto, string.Empty);
        }
        else
        {
            throw new  NotFoundException("User was not found! .",nameof(user));
        }
    }
}
