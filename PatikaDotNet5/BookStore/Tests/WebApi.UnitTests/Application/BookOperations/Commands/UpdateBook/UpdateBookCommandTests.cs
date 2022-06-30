
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.UpdateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using static WebApi.Application.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook{
    public class UpdateBookCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
     
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
            
        }

        [Fact]
              public void WhenNonExistentBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            Book book = new Book() { Title = "Charlinin melekleri", PageCount = 234, PublishDate = new System.DateTime(1990, 01, 10), GenreId = 1, AuthorId = 2 };
            _context.Books.Add(book);
            _context.SaveChanges();
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 12;
            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Guncellenecek kitap bulunamadi");

        }


         [Fact]
        public void When_ExitsBookIdIsGiven_Book_ShoulBeUpdated()
        {
            Book book = new Book() { Title = "Charlinin melekleri", PageCount = 234, PublishDate = new System.DateTime(1990, 01, 10), GenreId = 1, AuthorId = 2 };
            _context.Books.Add(book);
            _context.SaveChanges();
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookModel model = new UpdateBookModel() { Title = "Hobbit",GenreId=1 };
            command.Model=model;
            command.BookId=1;

            //Act
            FluentActions
            .Invoking(() => command.Handle()).Invoke();
           
            var bookObj=_context.Books.SingleOrDefault(b=>b.Title==model.Title);
            //burda zaten title a ait bilgi bulamazsa o zamn book null gelir ve NotBeNull() da patlar zaten
            //Title i kontrol etmeye gerek yok...o yuzden zaten SingleOrDefault ile kontrol etis oluyoruz
            //book u olusturuldugunu anlamak icin herseyini kontrol edecegiz....
            bookObj.Should().NotBeNull();
            bookObj.Title.Should().Be(model.Title);
            bookObj.GenreId.Should().Be(model.GenreId);
         
            
          
        }
    }
}
/*
public void Handle(){
            var book=_dbContext.Books.SingleOrDefault(book=>book.Id==BookId);
        if(book is null)throw new InvalidOperationException("Guncellenecek kitap bulunamadi");  
        Normalde hem 0 degilse yani bos degilse hemde ayni degilse diye bir kontrol yapmak lazm iste onu default keywordunu kullanarak yapiyoruz
            book.GenreId=Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title=Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }
*/