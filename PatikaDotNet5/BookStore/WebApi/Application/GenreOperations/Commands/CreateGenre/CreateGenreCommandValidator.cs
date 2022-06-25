

using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.CreateGenre {
    public class CreateGenreCommandValidator:AbstractValidator<CreateGenreCommand> 
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command=>command.Model.Name).NotEmpty().MinimumLength(4);
         
        }

    }
}