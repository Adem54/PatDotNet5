
using System;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetail {

    public class GetBookDetailQuery{

        public int BookId {get; set;}
        private BookSellerDbContext _dbContex;
        public GetBookDetailQuery(BookSellerDbContext dbContext)
        {
            _dbContex=dbContext;
        }

       public BookDetailViewModel Handle(){
        var book=_dbContex.Books.SingleOrDefault(book=>book.Id==BookId);
        if(book is null)throw new InvalidOperationException("Kitap zaten mevcut");
        BookDetailViewModel vm=new BookDetailViewModel();
        vm.Title=book.Title;
        vm.Genre=((GenreEnum)book.GenreId).ToString();
        vm.PublishDate=book.PublishDate.Date.ToString("dd/MM/yyyy");
        return vm;
        
       } 

    public class BookDetailViewModel {
        public string Title {get; set;}
        public string Genre {get; set;}
        public int PageCount {get; set;}
        public string PublishDate {get; set;}
    }


    }
}