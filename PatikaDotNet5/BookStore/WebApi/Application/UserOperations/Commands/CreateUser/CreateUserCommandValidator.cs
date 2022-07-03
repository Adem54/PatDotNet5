


using System.Text.RegularExpressions;
using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Model.FirstName).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(8);
            RuleFor(command => command.Model.Email).NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("A valid email is requred");
            RuleFor(command => command.Model.Password)
            .Length(5, 15)
            .Must(x => HasValidPassword(x))
            .WithMessage("Passwords must match");
        }

        private bool HasValidPassword(string pw)
        {
            var lowercase = new Regex("[a-z]+");
            var uppercase = new Regex("[A-Z]+");
            var digit = new Regex("(\\d)+");
            var symbol = new Regex("(\\W)+");
            return (lowercase.IsMatch(pw) && uppercase.IsMatch(pw) && digit.IsMatch(pw) && symbol.IsMatch(pw));
        }

    }
}
