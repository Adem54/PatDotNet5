
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail {
   public class GetBookDetailQueryValidatorTests {

      [Theory]

            [InlineData(0)]
            [InlineData(-1)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int id)
        {
             GetBookDetailQuery query=new GetBookDetailQuery(null,null);
             query.BookId=id;
            
            GetBookDetailQueryValidator validator=new GetBookDetailQueryValidator();
            var result=validator.Validate(query);
             result.Errors.Count.Should().BeGreaterThan(0);
        }

          [Theory]

            [InlineData(1)]
            [InlineData(2)]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError(int id)
        {
             GetBookDetailQuery query=new GetBookDetailQuery(null,null);
             query.BookId=id;
            GetBookDetailQueryValidator validator=new GetBookDetailQueryValidator();
            var result=validator.Validate(query);
             result.Errors.Count.Should().Equals(0);
        }

   }
}
