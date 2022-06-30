using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor{
    public class UpdateAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
     
        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
            
        }

        [Fact]
              public void WhenNonExistentAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var author = new Author() { FirstName = "Palermo", LastName="Geovanni", BirthDate = new System.DateTime(1990, 01, 10) };
            _context.Authors.Add(author);
            _context.SaveChanges();
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 12;
            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Guncellenecek kitabin yazari bulunamadi");

        }


         [Fact]
        public void When_ExitsBookIdIsGiven_Author_ShoulBeUpdated()
        {
              var author = new Author() { FirstName = "Palermo", LastName="Geovanni", BirthDate = new System.DateTime(1990, 01, 10) };
            _context.Authors.Add(author);
            _context.SaveChanges();
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            UpdateAuthorModel model = new UpdateAuthorModel() { FirstName = "Sergio",LastName="Raqel", BirthDate=DateTime.Now.Date.AddYears(-45) };
            command.Model=model;
            command.AuthorId=1;

            //Act
            FluentActions
            .Invoking(() => command.Handle()).Invoke();
           
            var authorObj=_context.Authors.SingleOrDefault(a=>a.LastName==model.LastName);
            //burda zaten title a ait bilgi bulamazsa o zamn book null gelir ve NotBeNull() da patlar zaten
            //Title i kontrol etmeye gerek yok...o yuzden zaten SingleOrDefault ile kontrol etis oluyoruz
            //book u olusturuldugunu anlamak icin herseyini kontrol edecegiz....
            authorObj.Should().NotBeNull();
            authorObj.FirstName.Should().Be(model.FirstName);
            authorObj.LastName.Should().Be(model.LastName);
            authorObj.BirthDate.Should().Be(model.BirthDate);
         
            
          
        }
    }
}