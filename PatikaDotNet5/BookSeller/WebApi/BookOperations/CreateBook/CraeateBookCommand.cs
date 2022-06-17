
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook {
    
    public class CreateBookCommand {

        public CreateBookModel Model {get; set;}  
        //Oncelikle bu CreateBookModel tipindeki Book Model i kullanicidan bize Controller
    //a gelecek ilk olarak, yani icinde kullanicinn gonderdigi data ile gelecek
    //Yani bu data dolu gelecek icinde data si var olarak gelecek ve biz de burda direk
    //Model e gelen data nin Title i acaba veritabanindaki Book Title lar ile ayni olan var mi
    //diye sorgulama yapabilecegiz...  Cunku gelen controller a kullanicidan gelen data
    // burdaki class imiz new lendikten sonra, CreateBookCommand new lendikten sonra 
    //Model properties ini atama yapilacak controller icerisinde ondan dolayi da biz Model.Title
    //deyince, kullanicini gondermis oldugu Title i alabilmmis olacagiz...
        private readonly BookSellerDbContext _dbContext;
        public CreateBookCommand(BookSellerDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public void Handle(){
            var book=_dbContext.Books.SingleOrDefault(book=>book.Title==Model.Title);
            //Dikkat edelim Model ici dolu olarak geliyor buraya,Controller da parametre olarak geliyor
            //Ardindan orda, var createCommand=new CreateBookCommand(_context);ve createCommand.Model=newBook
            //,newBook paramtereden gelen CreateBookModel tipindeki kullanicidan gelen datadir
            //  Controller icinde ici doldurulmus olacak
            //Ve burda o model den alacagiz datayi ve de, Book entitysine yazdirarak veritabanina kaydetme
            //islemin burda yapacagiz....
            if(book is not null)throw new InvalidCastException("Kitap zaten mevcut");
           //Simdi biz bize Controllerda atamasi yapilan Model icindeki datalardan Book entity mizden
           //bir tane instance olusturup propertieslerini set edecegiz ve ondan sonra o set ettigmiz
           //book entity instancesini veritabanina kaydedecegiz...
            book=new Book();
            book.Title=Model.Title;
            book.PublishDate=Model.PublishDate;
            book.PageCount=Model.PageCount;
            book.GenreId=Model.GenreId;

            //book entitymizden olusturdugmuz, instanceye Model uzerinden, kullanicidan gelen
            //datalari propertieslere set ettikten sonra artik, veritabanina book instancesini 
            //ekleyebiliriz
            _dbContext.Add(book);
            _dbContext.SaveChanges();

        }


        public class CreateBookModel 
        {
                public string Title {get; set;}
                public int GenreId {get; set;}//int olarak almam lazm, int value ye ihtiyacim var...
                public int PageCount {get; set;}
                public DateTime PublishDate {get; set;}
        }
    }
}