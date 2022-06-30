

using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            List<Book> bookList = testFixture.Context.Books.ToList();
            _mapper = testFixture.Mapper;
        }

        [Fact]
        //Burda silme islemi yaparken, database de var  olmayan bir id yi input olarak gonderip silmeye
        //calisirsa hata veriyor mu onu kontrol ederiz,yani yazdigmiz hata firlatma kismi dogru calisiyor mu
        //onu test ediyoruz
        public void WhenNonExistentBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            Book book = new Book() { Title = "Charlinin melekleri", PageCount = 234, PublishDate = new System.DateTime(1990, 01, 10), GenreId = 1, AuthorId = 2 };
            _context.Books.Add(book);
            _context.SaveChanges();
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 12;
            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadi");

        }

        // public void WhenValidInputIsGiven_Book_ShouldBeCreated()
        //Burda da, database de var olan bir id gonderince sistem calisiyor mu data yi siliyor mu
        //onu kontrol edelim, yani once Handle methodunu calistiiralim sonra da veritabanindna data silinmis mi onu kontrol edelim

        [Fact]
        public void When_ExitsBookIdIsGiven_Book_ShoulBeDeleted()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            Book myBook = new Book() { Title = "Charlinin melekleri", PageCount = 234, PublishDate = new System.DateTime(1990, 01, 10), GenreId = 1, AuthorId = 2 };
            _context.Books.Add(myBook);
            _context.SaveChanges();
            command.BookId = 1;
            FluentActions.Invoking(() => command.Handle()).Invoke();
            var book = _context.Books.SingleOrDefault(b => b.Id == command.BookId);

            book.Should().BeNull();
        }
    }
}
/*
  public void Handle()
        {
            var book=_dbContext.Books.SingleOrDefault(book=>book.Id==BookId);
            if(book is null)throw new InvalidOperationException("Silinecek kitap bulunamadi");
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
Burda ilk once biz gelen id nin bizde var olan id ler icinde olup olmadingin kontrol eden
method dogru calisiyor mu onu test edecegiz yani bizde olmayan bir id geldiginde
hata gonderiyor mu onu bir test edelim...
*/