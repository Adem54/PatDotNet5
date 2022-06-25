

using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre{
    public class UpdateGenreCommandValidator:AbstractValidator<UpdateGenreCommand> {

          public UpdateGenreCommandValidator()
            {
                RuleFor(command=>command.GenreId).GreaterThan(0);
    
                //Minimum length i Name i bos olmaz ise 4 olsun diyoruz, yanbir validasyon kuralini kosula
                //baglamis oluyoruz
                RuleFor(command=>command.Model.Name).MinimumLength(4).When(x=>x.Model.Name!=string.Empty);
            }

    }
} 