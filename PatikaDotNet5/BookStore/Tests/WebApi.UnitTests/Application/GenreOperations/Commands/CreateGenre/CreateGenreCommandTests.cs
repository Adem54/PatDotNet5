
using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using static WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {    
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
      
        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre() { Name = "Fantasii" };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            //Hata verip vermedigini anlamak icin
            command.Model = new CreateGenreModel() { Name =genre.Name };

            FluentActions
            .Invoking(() => command.Handle())             
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Kitap turu zaten mevcut");
        }


         [Fact]
        public void WhenValidInputIsGiven_Book_ShouldBeCreated()
        {
           
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            CreateGenreModel model = new CreateGenreModel() {Name = "Fantasii"};
            command.Model=model;

            //Act
            FluentActions
            .Invoking(() => command.Handle()).Invoke();
            
            var genre=_context.Genres.SingleOrDefault(g=>g.Name==model.Name); 
            genre.Should().NotBeNull();
        
        }

    }
}