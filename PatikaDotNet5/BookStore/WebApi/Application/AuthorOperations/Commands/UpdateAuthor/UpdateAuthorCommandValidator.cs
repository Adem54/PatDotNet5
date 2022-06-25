

using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor{
    public class UpdateAuthorCommandValidator:AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command=>command.AuthorId).NotEmpty().GreaterThan(0);
            RuleFor(command=>command.Model.FirstName).NotEmpty().MinimumLength(4);
            RuleFor(command=>command.Model.LastName).NotEmpty().MinimumLength(2);
            RuleFor(command=>command.Model.BirthDate).NotEmpty().LessThan(DateTime.Now.Date);
            
        }
    }
}