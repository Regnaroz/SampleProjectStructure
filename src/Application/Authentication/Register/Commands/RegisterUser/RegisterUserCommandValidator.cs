using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Application.Authentication.Register.Commands.RegisterUser;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(v => v.FirstName).NotEmpty().MinimumLength(3).MaximumLength(64);
        RuleFor(v => v.LastName).NotEmpty().MinimumLength(3).MaximumLength(64);
        RuleFor(v => v.Email).NotEmpty().MinimumLength(5).MaximumLength(64);
        RuleFor(v => v.Password).NotEmpty().MinimumLength(8).MaximumLength(64);
    }
}
