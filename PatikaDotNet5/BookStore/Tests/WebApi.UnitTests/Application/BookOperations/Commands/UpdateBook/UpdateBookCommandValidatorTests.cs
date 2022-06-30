

using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.UpdateBook;
using static WebApi.Application.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{

    public class UpdateBookCommandValidatorTests
    {


        [Theory]
        //Inline data vermek istiyoruz,test calisirken, parametrede setlenmesi gereken degerleri
        //inline olarak veriyoruz
        [InlineData("Lord Of The Rings",0,1)]//Bu degerleri parametre deki degiskenlere set edilecek test yaparken
        [InlineData("",1,0)]
        [InlineData("", 0,1)]
        [InlineData("", 0,0)]
        [InlineData("Honny Moon", 1,0)]
        [InlineData("Honny Moon", 0,0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int genreId,int bookId)
        {

            UpdateBookCommand command = new UpdateBookCommand(null);
           //Artik datalari statik degil de gelen veriye bagli olarak setlenmesini istiyoruz
            command.Model = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId,
            };
            command.BookId=bookId;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
              result.Errors.Count.Should().BeGreaterThan(0);
        }




         [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
             UpdateBookCommand command = new UpdateBookCommand(null);
            //Tum degerleri valid vererek hata almadan calistigimizi test edecegiz burda
               command.Model = new UpdateBookModel()
            {
                Title = "Lord Of The Rings",
                GenreId = 1,
            };
            command.BookId=1;

             UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Equals(0);//Burda da hic hata vermemesini test et diyoruz...
        }
    }
}

/*

RuleFor(command=>command.BookId).GreaterThan(0);
RuleFor(command=>command.Model.GenreId).GreaterThan(0);
PublisDate bos olmasin ve bugunden de kucuk olsun, yani gecmiste olmali
RuleFor(command=>command.Model.Title).NotEmpty().MinimumLength(4);
En az 4 karakter olsun

*/