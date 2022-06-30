
using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;


namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre{
    public class UpdateGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
     
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
            
        }

        [Fact]
              public void WhenNonExistentGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            Genre Genre = new Genre() { Name = "Science-fiction"};
            _context.Genres.Add(Genre);
            _context.SaveChanges();
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 12;
            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Guncellenmek istenen kitap turu mevcut degil");

        }


         [Fact]
        public void When_ExitsGenreIdIsGiven_Genre_ShoulBeUpdated()
        {
              Genre Genre = new Genre() { Name = "Science-fiction"};
            _context.Genres.Add(Genre);
            _context.SaveChanges();
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreModel model = new UpdateGenreModel() { Name = "Real-life"};
            command.Model=model;
            command.GenreId=1;

            //Act
            FluentActions
            .Invoking(() => command.Handle()).Invoke();
            var genreObj=_context.Genres.SingleOrDefault(g=>g.Name==model.Name);
           
            genreObj.Should().NotBeNull();
            genreObj.Name.Should().Be(model.Name);
          
        }
    }
}