

using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks{

    public class GetBooksQuery{

        private readonly BookSellerDbContext _context;
        public GetBooksQuery(BookSellerDbContext context)
        {
            _context = context;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList=_context.Books.OrderBy(book=>book.Id).ToList();
            List<BooksViewModel> vm=new List<BooksViewModel>();
            foreach (var book in _context.Books)
            {
                 vm.Add(new BooksViewModel(){
                    Title=book.Title,
                    Genre=((GenreEnum)book.GenreId).ToString(),
                    //Id uzrinden Enum a karsilik gelen Enum degerini string olarak getirdik
                    PublishDate=book.PublishDate.Date.ToString("dd/MM/yy"),
                    //Aldgimiz uzun tarihin sadece dd/MM/yy kismin aliyorz string olarak
                    PageCount=book.PageCount

                 });
            }
            return vm;
        }


        public class BooksViewModel
        {
            public string Title {get; set;}
            public int PageCount {get; set;}
            public string PublishDate {get; set;}
            public string Genre {get; set;}
        }
    }
}

/*
Biz entity mizdeki datalari aynen oldugu gibi kullanici ile paylasmak istemiyorz onun yerine,
biz kullaniciya ihtiyaci olan, gormesi gereken datalari gormesi gereken formatta gosterecegiz...
Dolayisi ile biz dogrudan hoop database den gel kullaniciya git, olmamasi gerekir o zaamn biz ne yapacagiz
database den gelen Book listemizin bir modifasyojdan gecirip BookViewModel obje turunde kullanicya dondurecegiz
Genre de artik Id yerine, kullaniciya string donecegiz, bunu normalde eger Genre isminde bir database tablosu 
olusturup yapacak isek o zaman, Dto mantiginda bir tane, Dto isminde kullaniciya gostermek istedigmiz 
alanlardan olusan bir class olstururuz Book ve GEnre icindeki alanlardan secip sonra da 
Genre tablosu ile Book tablosunu join edip Select return olarak da bu icinden alanlari secip olusturdugmz
Dto class i olarak done de biliriz ama bu yaklasima gore biz Genre ile ilgili bir tablo olusturmadik
ve dogrudan Genre id uzerinden hangi tur filmler oldugunu yazacagiz yani Genre ile iligli bir tane Enum class
olusturacagiz ve _context.Books umuzdaki her bir book icerisinde datalari BookViewModel alanlarina gore dondururuz  
Bu javascriptte map ile yapiliyor yani tek tek _context.Books icindeki tum Book lari donderip her donmede
de new BookViewModel in propertylerine set islemi yaparak tum Book lari List<BookViewModel> icerisine ekleyecegiz..
  */