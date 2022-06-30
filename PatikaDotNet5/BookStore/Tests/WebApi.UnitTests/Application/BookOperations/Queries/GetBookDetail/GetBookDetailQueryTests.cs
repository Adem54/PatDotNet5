
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using static WebApi.Application.BookOperations.Queries.GetBookDetail.GetBookDetailQuery;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail {
    public class GetBookDetailQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
            _mapper=testFixture.Mapper;
        }

//Ilk olarak, database de olmayan bir id gonderildiginde hata verme islmeni bizim uygulamda
//yazdigmiz gibi verip vermedigni test ederiz....
        [Fact]
         public void WhenNonExistentBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            Book book = new Book() { Title = "Charlinin melekleri", PageCount = 234, PublishDate = new System.DateTime(1990, 01, 10), GenreId = 1, AuthorId = 2 };
            _context.Books.Add(book);
            _context.SaveChanges();
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = 12;
            FluentActions.Invoking(() => query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadi");

        }

      //Simdi de id database de olan bir id gonderildiginde, ve Handle metodu problemsiz calistginda
      //ne olmasi bekleniyorsa o olmasi beklenenin olup olmadigni test ederiz...  

          [Fact]
        public void When_ExitsBookIdIsGiven_BookViewModel_ShoulBeReturn()
        {
           GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
           Genre genre=new Genre(){Name="Historical"};
             Author author=new Author{
                    FirstName="Margaret",
                    LastName="Atwood",
                    BirthDate=new System.DateTime(1972,03,14)
                    };
            Book myBook = new Book() { Title = "Charlinin melekleri", PageCount = 234, PublishDate = new System.DateTime(1990, 01, 10), GenreId = 1, AuthorId = 1 };
            _context.Books.Add(myBook);
            _context.SaveChanges();
          //  BookDetailViewModel model=new BookDetailViewModel(){Title=myBook.Title,PageCount=myBook.PageCount,};

            query.BookId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();
            var book = _context.Books.Include(b=>b.Genre).Include(b=>b.Author).SingleOrDefault(b => b.Id == query.BookId);
            var bookDetailViewModel=new BookDetailViewModel(){Title=book.Title,Genre=book.Genre.Name,PageCount=book.PageCount,PublishDate=book.PublishDate.ToString(),AuthorName=book.Author.FirstName+book.Author.LastName};
            

            book.Should().NotBeNull();

          
           

        }
        
    }
}

/*

  public BookDetailViewModel Handle(){
            var book=_dbContext.Books.Include(x=>x.Genre).Include(x=>x.Author).Where(book=>book.Id==BookId).SingleOrDefault();
            if(book is null)
            throw new InvalidOperationException("Kitap bulunamadi");
      
            BookDetailViewModel vm=_mapper.Map<BookDetailViewModel>(book);
         
           return vm;
        }

 
        public class BookDetailViewModel {
            public string Title {get; set;}
            public string Genre {get; set;}
            public int PageCount {get; set;}
            public string PublishDate {get; set;}
            public string AuthorName {get; set;}
        }
*/