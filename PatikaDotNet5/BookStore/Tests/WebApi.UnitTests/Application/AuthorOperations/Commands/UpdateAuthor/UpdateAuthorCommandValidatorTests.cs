
using System;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{

    public class UpdateAuthorCommandValidatorTests
    {


        [Theory]
        //Inline data vermek istiyoruz,test calisirken, parametrede setlenmesi gereken degerleri
        //inline olarak veriyoruz
        
        [InlineData(" "," ", "1982/08/16")]//Bu degerleri parametre deki degiskenlere set edilecek test yaparken
        [InlineData("","","1999/04/19")]
        [InlineData("Denver", "Sierra","2022/10/03")]
        [InlineData("", "Cornel","1980/01/12")]
      
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string firstName, string lastName,string birthDate)
        {

            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
           //Artik datalari statik degil de gelen veriye bagli olarak setlenmesini istiyoruz
            command.Model = new UpdateAuthorModel()
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate=Convert.ToDateTime(birthDate)
            };
         
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
              result.Errors.Count.Should().BeGreaterThan(0);
        }




         [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
             UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            //Tum degerleri valid vererek hata almadan calistigimizi test edecegiz burda
               command.Model = new UpdateAuthorModel()
            {
                FirstName = "Kaia",
                LastName = "Johannes",
                BirthDate=new DateTime(1981,10,15),
            };
            command.AuthorId=1;

             UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Equals(0);//Burda da hic hata vermemesini test et diyoruz...
        }
    }
}