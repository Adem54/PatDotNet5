
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail {
   public class GetAuthorDetailQueryValidatorTests {

      [Theory]

            [InlineData(0)]
            [InlineData(-1)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int id)
        {
             GetAuthorDetailQuery query=new GetAuthorDetailQuery(null,null);
             query.AuthorId=id;
            
            GetAuthorDetailQueryValidator validator=new GetAuthorDetailQueryValidator();
            var result=validator.Validate(query);
             result.Errors.Count.Should().BeGreaterThan(0);
        }

          [Theory]

            [InlineData(1)]
            [InlineData(2)]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError(int id)
        {
             GetAuthorDetailQuery query=new GetAuthorDetailQuery(null,null);
             query.AuthorId=id;
            GetAuthorDetailQueryValidator validator=new GetAuthorDetailQueryValidator();
            var result=validator.Validate(query);
             result.Errors.Count.Should().Equals(0);
        }

   }
}