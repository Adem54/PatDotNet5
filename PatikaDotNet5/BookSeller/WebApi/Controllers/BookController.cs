
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
// using WebApi.BookOperations.GetBooks;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Controllers{

    [ApiController]//class imzin httpresponse donecegini soyluyoruz burda
    [Route("api/[controller]s")]
    /*
    Burasida responslarimizin hangi resource nin hangi endpoint main adress,route ismi ne 
    olacak burda onu ayarliyoruz controller ismi ile gelsin controller ismiimiz ne
     BookStore iste BookStore ismi ile gelsin demis oluyoruz...buraya istersek 
      [Route("[aoi/controller]s")] dersek o zaman da bu adress route ile karsilamis olacagiz...
    */
    public class BookController:ControllerBase {

        private readonly BookSellerDbContext _context;
        //Uygulama icerisinde degistirilemesin diye readonly yapariz....bu onemli...bunu iyi bilelim..

        public BookController(BookSellerDbContext context)
        {
         
            var test=new List<string>();
            _context = context;
           
        }
  /*
        Artik biz BookList emiz yerine _context.Books u kullanabiliriz....
        Tabi ki biz artik islemlerimiz static bir listede yapmiyoruz EntityframeworkCore icinde 
        yapiyoruz o zaman ne yapacagiz CRUD islemlerimizin hepsinini sonunda database e kaydetme 
        islemi olan Savechanges islemini yapmamiz gereior.UNITOFWORK mantiginda isemimizi yapmamiz gerekiyor...
        TAbi biz eger database icinde degisiklik yaparsak yani database data ekleme, data guncelleme 
        ve data silme islemlerinde savechanges yapariz yoksa data listelemede oyle bir iseleme ihtiyacimiz yokk.....

       

*/

[HttpGet] //https://localhost:5001/Books
public IActionResult GetBooks(){
   
    // var bookList=_context.Books.OrderBy(book=>book.Id).ToList<Book>();
    // return bookList;
    GetBooksQuery query=new GetBooksQuery(_context);
    var result=query.Handle();
    return Ok(result);
    /*
    Biz artik burda hem 200 bilgisini don hemde, objeyi don diyecegiz..
    Tabi bunu yapabilemiz icin IActionResult dondurursek, tipten bagimsiz olarak
    http Ok bilgisi ile birlikte datayi da donmek istersek iste bu sekilde ActionResult doneriz..
    */
}

[HttpGet("{id}")]//https://localhost:5001/Books/1
public Book GetBookById(int id){
    var book=_context.Books.Where(book=>book.Id==id).SingleOrDefault();
    return book;
}

/*
Bir Controller icinde 1 tane [HttpGet] olmali kurali vardir Controller icinde ondan dolayi 2 kez [HttpGet] olursa hata aliriz..
[HttpGet]//https://localhost:5001/Books?id=3
public Book Get([FromQuery] string id){
    var book=BookList.Where(book=>book.Id==Convert.ToInt32(id)).SingleOrDefault();
    return book;
}
*/

   [HttpPost]

/*
Biz post, put, delete islemlerinde de kullaniciyi dogru yonlendirmek icin ve kullanicinin
 bir data si eklenemediginde neden dolayi eklenemiyor vs gibi dogru yonlendirebilmek 
 icin kullaniciya bir sey donmemiz gerekiyor ondan dolayi da bize .Net5 ten gelen IActionResult kullaniriz..
*/
/*
COOOK ONEMLI-BUNU YENI OGRENIYORUMMM
Burda biz input olarak parametremize Book book entity geliyor bu dogru degildir bu yanlis bir yaklasimdir
Kullanicini belki de Book entity si iicndeki data lardan 2 tanesine  ihtiyaci var ama biz tum datayi acmisiz
kullaniciya hem data guvenligi hem de performansi olumsuz etkileyecektir zamanla
Biz inputa Book u alarak ve Book u output olarak donerek de bir bagimlilik olusturuyorz aslinda
AddBook da da biz entity olarak baska bir model aliyor olmamiz gerekiyor, dogrudan entity yi input olarak almak
parametremize almak dogru bir yaklasim degildir
*/
   public IActionResult AddBook([FromBody]CreateBookModel newBook){
   CreateBookCommand command=new CreateBookCommand(_context);
   
   try
   {
   command.Model=newBook;
   command.Handle(); 
   return Ok();
   }
   catch (Exception ex)
   {
   return BadRequest(ex.Message); 
   
   }
 
   }
   //Burayi test ederken serverimizi tekrar bir kapatip calistiralim yoksa yaptgimiz islemler tam uygulanmayabiliyor


/*Normalde post isleminde yeni data eklenirken kullanici tarafindan id gonderilmez, 
id yi ya auto-increment yolu ile direk database den ya da Guid type kullanarak
 kendimiz direk entity class inda constructor icinde her yeni data eklendiginde 
 kendisi otomatik uniq id olustursun deriz ve bunlar back-endde oluyor dolayisi ile id bilgisi bize disardan gelmez....
//PUt ve delete islemlerinde de zaten kullanici bizim ona sundugmuz data listesi
 uzerinden bir urune veya book nesnesine tiklayacagi icin biz ona zaten o datanin 
 id sini gondermis oluyoruz o da bize o id ile tekrar geliyor ondan da dolayi,
  biz gonderilen id uzerinden cok rahat birsekidle islemimizi yapabiliyoruz
*/

/*
COOOK ONEMLI---BUNU ILK DEFA OGRENIYORUMM....
UpdateBook disardan Book book entity si aliyor, biz ne dedik entityleri,modelleri,view modelleri
olabildigince birbrinden ayiralim ve ayri tutalim ki bagimliliklariimzi minimuma indirelim
ve proje buyuyunce hersey kontrolumuzden cikmasin...
*/
[HttpPut("{id}")]

public IActionResult UpdateBook(int id,[FromBody]Book updatedBook){
 var book=_context.Books.SingleOrDefault(book=>book.Id==id);
 if(book is null){
    return BadRequest();
 }   
 //Normalde hem 0 degilse yani bos degilse hemde ayni degilse diye bir kontrol yapmak lazm iste onu default keywordunu kullanarak yapiyoruz
 book.GenreId=updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
 book.Title=updatedBook.Title != default ? updatedBook.Title : book.Title;
 book.PageCount=updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
 book.PublishDate=updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
 //SwaggerUI da default olarak eger biz degistirmessek o kendisi, 
 //kendi guncel tarihini veriyor, ya da bos birakirsak o zaman da 001 gibi default tarih ataniyor 
    _context.SaveChanges();
 return Ok();
}

[HttpDelete("{id}")]

public IActionResult DeleteBook(int id){
var book=_context.Books.SingleOrDefault(book=>book.Id==id);
if(book is null){
    return BadRequest();
}
_context.Books.Remove(book);
_context.SaveChanges();
return Ok();
}

    }
}


/*
Biz burdaki operasyonlarimizi daha saglikli yonetebilmek, ve yapacagimiz islemlerin
entity,model ve view modellerini birbirinden ayirabilmek icin ayri bir klasor olusturacagiz
BookOperations isminde ve altina da yine ayri klasorlerde methodlarimizi olustururuz
*/
