
using System;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.UpdateBook{
    public class UpdateBookCommand{

        public int BookId {get; set;}
        public UpdateBookModel Model {get; set;}
        private readonly IBookStoreDbContext _dbContext;

        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public void Handle(){
            var book=_dbContext.Books.SingleOrDefault(book=>book.Id==BookId);
        if(book is null)throw new InvalidOperationException("Guncellenecek kitap bulunamadi");  
        //Normalde hem 0 degilse yani bos degilse hemde ayni degilse diye bir kontrol yapmak lazm iste onu default keywordunu kullanarak yapiyoruz
            book.GenreId=Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title=Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }

        //Burda biz bir tane model olusturacagiz, kullanicidan gelencek olan datalari barindiracak olan model
        //Yani biz direk entity yi kullanmiyoruz..
        //Model update icin cok daha anlamlidir, cunku bir entity icindeki her alan update edilmeyebilir
        //Modellerde de hangi alanlar update edilebilir ise sadece onu udpate ettirmeliyiz..
        //Ve bu class tipinde data gelecek, controller da parametreye ve controller da gelen data yi
        //hemen UpdateBookCommand sinifindan instance olusturup onun Model propertiesi set edilcek gelen data ile
        //VE biz Model i direk, UpdatecommandQuery class inda kullaniyoruz zaten...onun iicn controllerda set 
        //edilmelidir, parametreden gelen data ile

        public class UpdateBookModel {
            public string Title {get; set;}
            public int GenreId {get; set;}
            //PublishDate ve PageCount update edilemesin istiyoruz mesela
            //Biz update e sadece 2 alani aciyoruz...
            
        }

    }
}