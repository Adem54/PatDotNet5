

using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook{
    public class DeleteBookCommandValidatorTests{

            [Theory]

            [InlineData(0)]
            [InlineData(-1)]
         public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int id){
            DeleteBookCommand command=new DeleteBookCommand(null);
            command.BookId=id;    
            DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
            var result=validator.Validate(command);
            result.Errors.Count.Should().Equals(1);
         }

                [Theory]

            [InlineData(1)]
            [InlineData(2)]
          public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError(int id){
            DeleteBookCommand command=new DeleteBookCommand(null);
            command.BookId=id;    
            DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
            var result=validator.Validate(command);
            result.Errors.Count.Should().Equals(0);
          }
    }
}