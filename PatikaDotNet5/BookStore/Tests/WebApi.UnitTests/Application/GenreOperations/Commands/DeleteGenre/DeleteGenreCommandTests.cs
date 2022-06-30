
using FluentAssertions;
using WebApi.Application.BookOperations.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;


namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre{
    public class DeleteGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
            
        }

        [Fact]
              public void WhenNonExistentGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            Genre Genre = new Genre() { Name = "Science-fiction"};
            _context.Genres.Add(Genre);
            _context.SaveChanges();
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 12;
            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap turu bulunamadi");

        }


         [Fact]
        public void When_ExitsGenreIdIsGiven_Genre_ShoulBeUpdated()
        {
              Genre Genre = new Genre() { Name = "Science-fiction"};
            _context.Genres.Add(Genre);
            _context.SaveChanges();
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId=1;

            //Act
            FluentActions
            .Invoking(() => command.Handle()).Invoke();
            var genreObj=_context.Genres.SingleOrDefault(g=>g.Id==command.GenreId);
           
            genreObj.Should().BeNull();
          
        }
    }
}