


using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        
            _mapper = testFixture.Mapper;
        }

        [Fact]
        //Burda silme islemi yaparken, database de var  olmayan bir id yi input olarak gonderip silmeye
        //calisirsa hata veriyor mu onu kontrol ederiz,yani yazdigmiz hata firlatma kismi dogru calisiyor mu
        //onu test ediyoruz
        public void WhenNonExistentAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
      var author = new Author() { FirstName = "Palermo", LastName="Geovanni", BirthDate = new System.DateTime(1990, 01, 10) };
            _context.Authors.Add(author);
            _context.SaveChanges();
             DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId=12;
            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek yazar bulunamadi");

        }

        // public void WhenValidInputIsGiven_Book_ShouldBeCreated()
        //Burda da, database de var olan bir id gonderince sistem calisiyor mu data yi siliyor mu
        //onu kontrol edelim, yani once Handle methodunu calistiiralim sonra da veritabanindna data silinmis mi onu kontrol edelim

        /*
        
          var book=_dbContext.Books.Include(x=>x.Author).SingleOrDefault(b=>b.AuthorId==AuthorId && b.PublishDate < DateTime.Now);
            if(book is not null)throw new InvalidOperationException("Yayinda kitabi olan bir yazari silemezsiniz...");*/
        //Yayinda kitabi olan yazar silinememeli
        [Fact]
        public void When_AuthorHasBookWhichIsPublished_Author_ShouldNotBeDeleted()
        {
                var genre=new Genre(){Name="History"};
            var author = new Author() { FirstName = "Kevin", LastName="Andreas", BirthDate = new System.DateTime(1987, 01, 10) };
             var book=new Book(){Title="Sessiz Gemi",PageCount=256,PublishDate=new System.DateTime(1999,10,17),GenreId=1,AuthorId=1}; 
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId=1;
              FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yayinda kitabi olan bir yazari silemezsiniz...");
        }

        [Fact]
        public void When_ExitsAuthorIdIsGiven_Author_ShoulBeDeleted()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            // var genre=new Genre(){Name="History"};
            var author = new Author() { FirstName = "Kevin", LastName="Andreas", BirthDate = new System.DateTime(1987, 01, 10) };
            // var book=new Book(){Title="Sessiz Gemi",PageCount=256,PublishDate=new System.DateTime(1999,10,17),GenreId=1,AuthorId=0};

            _context.Authors.Add(author);
            _context.SaveChanges();
            command.AuthorId = 4;//Author u ekledik ama, bunu hic bir kitaba yazar olarak eklemedik...
            FluentActions.Invoking(() => command.Handle()).Invoke();
            var myAuthor = _context.Authors.SingleOrDefault(a => a.Id == command.AuthorId);

            myAuthor.Should().BeNull();
        }
    }
}
