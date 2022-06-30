
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{

    public class UpdateGenreCommandValidatorTests
    {


        [Theory]
      
        [InlineData("Historical",0)]//Bu degerleri parametre deki degiskenlere set edilecek test yaparken
        [InlineData("",1)]
        [InlineData("",0)]
        
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, int genreId)
        {

            UpdateGenreCommand command = new UpdateGenreCommand(null);
           //Artik datalari statik degil de gelen veriye bagli olarak setlenmesini istiyoruz
            command.Model = new UpdateGenreModel()
            {
                Name = name,
              
            };
            command.GenreId=genreId;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
              result.Errors.Count.Should().BeGreaterThan(0);
        }




         [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
             UpdateGenreCommand command = new UpdateGenreCommand(null);
            //Tum degerleri valid vererek hata almadan calistigimizi test edecegiz burda
               command.Model = new UpdateGenreModel()
            {
                Name = "Real-life",
               
            };
            command.GenreId=1;

             UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Equals(0);//Burda da hic hata vermemesini test et diyoruz...
        }
    }
}