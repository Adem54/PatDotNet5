
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;

        }
        
        [Fact]//Bu bir test metodu oldugunu soylemis oluyoruz.....bu onemlidir...
        public void WhenAlreadyExistAuthorTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {

            var author = new Author() { FirstName = "Palermo", LastName="Geovanni", BirthDate = new System.DateTime(1990, 01, 10) };
            _context.Authors.Add(author);
            _context.SaveChanges();
         
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
        
            command.Model = new CreateAuthorModel() { FirstName =author.FirstName,LastName=author.LastName };

            
            FluentActions
            .Invoking(() => command.Handle())//bu method calistirildiginda     //InvalidOperationException bu hatayi firlatmali                  
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Ayni isimde yazar mevcut");
          
          
        }

         [Fact]//Burda yeni bir book objesi database e eklenecek mi onu  test ediyoruz
        public void WhenValidInputIsGiven_Author_ShouldBeCreated()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel() {  FirstName = "Nakata", LastName="Serar", BirthDate = new System.DateTime(1990, 01, 10) };

            command.Model=model;
             
            

            FluentActions
            .Invoking(() => command.Handle()).Invoke();
            var author=_context.Authors.SingleOrDefault(a=>a.FirstName==model.FirstName && a.LastName==model.LastName);
         
            author.Should().NotBeNull();
            author.FirstName.Should().Be(model.FirstName);
            // author.LastName.Should().Be(model.LastName);
            // author.BirthDate.Should().Be(model.BirthDate);
          
        }

    }
}