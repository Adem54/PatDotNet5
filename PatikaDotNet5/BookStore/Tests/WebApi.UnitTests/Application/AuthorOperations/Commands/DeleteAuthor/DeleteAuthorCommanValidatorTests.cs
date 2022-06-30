

using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor{
    public class DeleteAuthorCommandValidatorTests{

            [Theory]

            [InlineData(0)]
            [InlineData(-1)]
         public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int id){
            DeleteAuthorCommand command=new DeleteAuthorCommand(null);
            command.AuthorId=id;    
            DeleteAuthorCommandValidator validator=new DeleteAuthorCommandValidator();
            var result=validator.Validate(command);
            result.Errors.Count.Should().Equals(1);
         }

                [Theory]

            [InlineData(1)]
            [InlineData(2)]
          public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError(int id){
            DeleteAuthorCommand command=new DeleteAuthorCommand(null);
            command.AuthorId=id;    
            DeleteAuthorCommandValidator validator=new DeleteAuthorCommandValidator();
            var result=validator.Validate(command);
            result.Errors.Count.Should().Equals(0);
          }
    }
}