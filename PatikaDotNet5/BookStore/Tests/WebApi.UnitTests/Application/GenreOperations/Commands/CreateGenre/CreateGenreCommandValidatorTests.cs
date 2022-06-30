

using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using static WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{


    public class CreateGenreCommandValidatorTests
    {

            [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
        {
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel()
            {
               Name = " "
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);

        }




         [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
             CreateGenreCommand command = new CreateGenreCommand(null, null);
            //Tum degerleri valid vererek hata almadan calistigimizi test edecegiz burda
               command.Model = new CreateGenreModel()
            {
               Name="Fantasi"
            };

             CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Equals(0);//Burda da hic hata vermemesini test et diyoruz...
        }
    }
}