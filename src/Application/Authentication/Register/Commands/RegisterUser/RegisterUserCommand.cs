using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleProject.Application.Authentication.Common;

namespace SampleProject.Application.Authentication.Register.Commands.RegisterUser;
public record RegisterUserCommand(
        string FirstName,
        string LastName,
        string Password,
        string Email)
    :  IRequest<AuthenticationResult>;
