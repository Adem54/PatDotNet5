
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail {
   public class GetGenreDetailQueryValidatorTests {

      [Theory]

            [InlineData(0)]
            [InlineData(-1)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int id)
        {
             GetGenreDetailQuery query=new GetGenreDetailQuery(null,null);
             query.GenreId=id;            
            GetGenreDetailQueryValidator validator=new GetGenreDetailQueryValidator();
            var result=validator.Validate(query);
             result.Errors.Count.Should().BeGreaterThan(0);
        }

          [Theory]

            [InlineData(1)]
            [InlineData(2)]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError(int id)
        {
             GetGenreDetailQuery query=new GetGenreDetailQuery(null,null);
             query.GenreId=id;
            GetGenreDetailQueryValidator validator=new GetGenreDetailQueryValidator();
            var result=validator.Validate(query);
             result.Errors.Count.Should().Equals(0);
        }

   }
}