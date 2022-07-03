
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Aoplication.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperatins.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.Application.BookOperatins.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.BookOperations.Queries.GetBookDetail.GetBookDetailQuery;
using static WebApi.Application.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Controllers{

    [Authorize]
//using Microsoft.AspNetCore.Authorization;

    [ApiController]//class imzin httpresponse donecegini soyluyoruz burda
    [Route("api/[controller]s")]
    /*
    Burasida responslarimizin hangi resource nin hangi endpoint main adress,route ismi ne 
    olacak burda onu ayarliyoruz controller ismi ile gelsin controller ismiimiz ne
     BookStore iste BookStore ismi ile gelsin demis oluyoruz...buraya istersek 
      [Route("[aoi/controller]s")] dersek o zaman da bu adress route ile karsilamis olacagiz...
    */
    public class BookController:ControllerBase {

        private readonly IBookStoreDbContext _context;
        //Uygulama icerisinde degistirilemesin diye readonly yapariz....bu onemli...bunu iyi bilelim..

        private readonly IMapper _mapper;
        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
           
        }


[HttpGet] //https://localhost:5001/Books
public IActionResult GetBooks(){
   
    // var bookList=_context.Books.OrderBy(book=>book.Id).ToList<Book>();
    // return bookList;
    GetBooksQuery query=new GetBooksQuery(_context,_mapper);
    //input um olmadigi icin, yani parametreye disardan deger gelmedigi icin
    //validasyon islemimiz olmayacak burda...
    var result=query.Handle();
    return Ok(result);
  
}

[HttpGet("{id}")]//https://localhost:5001/Books/1
public IActionResult GetBookById(int id){
        GetBookDetailQuery query=new GetBookDetailQuery(_context,_mapper);
        BookDetailViewModel result;
        query.BookId=id;
        GetBookDetailQueryValidator validator=new GetBookDetailQueryValidator();
        validator.ValidateAndThrow(query);
        result=query.Handle();    
    
    return Ok(result);
}

   [HttpPost]
   public IActionResult AddBook([FromBody]CreateBookModel newBook){
    CreateBookCommand command=new CreateBookCommand(_context,_mapper);
    //try-catch leri biz burdan kaldiriyoruz cunku, middleware de
    //handle edecegiz burdan firlatilacak bir hata yi ve 
    //de o hata yi da json olarak loglayacagiz..yazdiracagiz
    
        command.Model=newBook;
        //Validasyonu burda yapacagiz..
       CreateBookCommandValidator validator=new CreateBookCommandValidator();
        //ValidationResult result=validator.Validate(command);
        validator.ValidateAndThrow(command);
        command.Handle();
       
    return Ok();
   }
   //Burayi test ederken serverimizi tekrar bir kapatip calistiralim yoksa yaptgimiz islemler tam uygulanmayabiliyor

/*
COOOK ONEMLI---BUNU ILK DEFA OGRENIYORUMM....
UpdateBook disardan Book book entity si aliyor, biz ne dedik entityleri,modelleri,view modelleri
olabildigince birbrinden ayiralim ve ayri tutalim ki bagimliliklariimzi minimuma indirelim
ve proje buyuyunce hersey kontrolumuzden cikmasin...
*/
[HttpPut("{id}")]
public IActionResult UpdateBook(int id,[FromBody]UpdateBookModel updatedBook){
  
        UpdateBookCommand command=new UpdateBookCommand(_context);
         command.BookId=id;
         command.Model=updatedBook;
         UpdateBookCommandValidator validator=new UpdateBookCommandValidator();
         validator.ValidateAndThrow(command);
         command.Handle();
     return Ok();
}

[HttpDelete("{id}")]

    public IActionResult DeleteBook(int id){
                DeleteBookCommand command=new DeleteBookCommand(_context);
                command.BookId=id; 
                //Burda validation i BookId yi setledikten sonra ve de Handle methodundan
                //once yapmamiz gerekir...BookId yi setlemeden once verirsek, hata verir 
                //cunku int bir alan ve default olarak 0 verecek
                DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            return Ok();
    }

    }
}


/*
Biz burdaki operasyonlarimizi daha saglikli yonetebilmek, ve yapacagimiz islemlerin
entity,model ve view modellerini birbirinden ayirabilmek icin ayri bir klasor olusturacagiz
BookOperations isminde ve altina da yine ayri klasorlerde methodlarimizi olustururuz
*/
/*
AUTOMAPPER
Farkli tipteki kompleks objeleri birbirlerine otomatik olarak donduren bir kutuphanedir
Bizi tek tek mapleme isleminden kurtarir
 book=new Book(); 
                        book.Title=Model.Title;
                        book.PublishDate=Model.PublishDate;
                        book.PageCount=Model.PageCount;
                        book.GenreId=Model.GenreId;
bizi kod kirliliginden de kurtariyor ve tek satirda objenin kendisini dondurmemizi sagliyor
AutoMapper i projemize ekleyip, projemizi, endpointlerimiz automapper uzerinden calisir hale getirecegiz..
www.nuget.org a gideriz ve orda AutoMapper i bulup Dotnet CLI uzerinden yuklenecek kod satirini aliriz
dotnet add package AutoMapper --version 10.1.1

Biz ayrica AutoMapper i controller in constructor ina dependency injection  ile bu paketi gecip kullancagiz
Ondasn dolayi da ayrica bir de
AutoMapper.Extensions.Microsoft.DependencyInjection
bunu da kullanmamiz gerekiyor....
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 8.1.1
bu paketi de yukleriz
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 8.1.1

1.Öncelikle Automapper kütüphanesinin projeye dahil edilmesi gerekir.
AutoMapper paketi için aşağıdaki kod satırının .csproj dosyasının olduğu dizinde çalıştırılması gerekir.
dotnet add package AutoMapper --version 10.1.1
AutoMapper Dependecy Injection Paketi için aşağıdaki kod satırının .csproj dosyasının olduğu 
dizinde çalıştırılması gerekir.

COOK ONEMI-DOTNET RESTORE U KULLANARAKDA PAKET YUKLEYEBILIRZ..BESTPRACTISE
Normalde biz csproj dosyasiin icerisinde paketleri yazip, dotnet restore diyerek de paketleri yukleyebiliriz
Ki zaten bir takim ile calisirken ornegin bizim lokalde yuikledigmiz paketleri push ettigimzde github a ve 
takim arkadaslarimizda kendi projelerine bendeki degisklikleri pull ettiklerinde, bizim WebApi.scproj dosyazmiz
icnde yukledigmiz paket isimleri gidecek ama paketler yuklenmemis olacak dolayisi ile onlarda bendeki paketleri
yuklemek icin dotnet restore yapmalari gerekir, direk benim kullandigmiz paketleri kendi lokallerinde 
yukleyebilmeleri icin

COOK ONEMLI
Biz Autmapper paketini nuget ile  yukledim eyvallah ama bunu Dotnet in calistirabilmesi ve sisteme dahil edeblmesi icin
Startup.cs icindeki Configurasyonda servislere bunu eklemem lazm bu hep boyledir uygulamada kullanacagim disardan yeni bir
paket var ise o paketi nuget ten yukledikten sonraki adim im o paketi git Startup.cs ye ekle olacaktir....
   services.AddAutoMapper(Assembly.GetExecutingAssembly());  

2. Proje içerisinde AutoMappper'ı servis olarak kullanabilmemiz için Startup.cs dosyası içerisindeki 
Configure Service metoduna aşağıdaki kod satırının eklenmesi gerekir.
services.AddAutoMapper(Assembly.GetExecutingAssembly());

3.Mapper Konfigürasyonu için Profile sınıfından kalıtım alan aşağıdaki gibi bir sınıf implemente etmemiz gerekir.
using AutoMapper;
using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.BookOperations.GetBookDetail;
using BookStoreWebApi.Entities;

    namespace BookStoreWebApi.Common
    {
        public class MappingProfile : Profile
        {
            public MappingProfile(){
                CreateMap<CreateBookModel, Book>();
                CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => 
                opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            }
        }


   Obje donusumlerinin configurasyonunu biryerde verirsek bundan sonra Mapper.Map yolu ile donusum yapabiliriz
   Dolayisi ile benim bir mapping profile dosyasinna ihtiyacim var   

     MappingProfile i AutoMap olmasini istiyorsak Profile sinifindan kalitim almasi gerekir
    ve artik MappingPrfoile bir automapperdir,tabi ki Profile Automapper dan geliyor
        public class MappingProfile:Profile {
            Simdi burda ne neye donusebilir onun configlerini veriyor oalcagiz...
            Controllerimizin icndeki, Create icin bakalim
            Controollerdaki Create ,AddBook endpointinie bir tane mapper gonderelim injection yolu ile
            public MappingProfile(){
                CreateMap<CreateBookModel,Book>();
            }
            Ilk parametre source, 2.parametre targettir
            Burda demis oluyoruz ki CreateBookModel objesi Book entity objesine maplenebillir olsun diyoruz
            Burda biz CreateBookModel objesi ile disardan datayi aliyoruz ve sonra Book entity objesine
             datalari aktariyorduk,, iste kaynagimiz CreateBookModel, targetimiz ise Book olacak
        }

4.Eklemiş olduğumuz Dependency Injection paketi sayesinde Controller'ın kurucu fonksiyonunda mapper'ı 
kod içerisinde kullanılmak üzere dahil edebiliriz.

private readonly IMapper _mapper;
public BookController(BookStoreDbContext context, IMapper mapper)
{
    _context = context;
    _mapper = mapper;
}

  public IActionResult AddBook([FromBody]CreateBookModel newBook){
    CreateBookCommand command=new CreateBookCommand(_context,_mapper);

    CreateBookCommand class ina da enjekt ederiz cunku mapping islemini orda yapacagiz

    public class CreateBookCommand {
     private readonly IMapper _mapper;
            public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
            {
                _dbContext=dbContext;
                _mapper=mapper;
            }

 AUTOMAPPERIN KULLANACAGIMIZ YER TAM OLARAK BURDAKI 4 SATIR YERINE TEK SATIRDA BU ISI YAPMAK 
                        CreatBookMode tipinde olan Model ile Book id haric ayni datalara sahiptirler.Dolayisi ile biz
                        dicez ki CreateBookModel objesi Book objesine maplenebilir, donusturulebilir demem lazm
                        Bunun iicinde bunu MappinProfile icinde belirtmemiz gerekir
                        Burda biz CreateBookModel objesi ile disardan datayi aliyoruz ve sonra Book entity objesine
                        datalari aktariyorduk,, iste kaynagimiz CreateBookModel, targetimiz ise Book olacak
                          book=new Book(); 
                        book.Title=Model.Title;
                        book.PublishDate=Model.PublishDate;
                        book.PageCount=Model.PageCount;
                        book.GenreId=Model.GenreId;
                        book=_mapper.Map<Book>(Model);
                        Map<targetObje>() source uzerinden de tipi olacak, model ile gelen veriyi book objesi icerisine
                        o verileri maple, convert et demektir nerden faydalaniyor MappingPfofile da yaptimgz o configurasyondan 
                        faydalaniyor
                        Endpointimizin calisip calismadigni test ettgimiz zaman end-pointimizin calistigni gorebiliriz....

                        5.Artık kod içerisinde _mapper'ı kullanabiliriz.

Profile sınıfından kalıtım alan sınıfa (Yukarıdaki örnekte MappingProfile) daha yakından bakmakta fayda var. 
Çünkü mapping konfigurasyonlarımız o sınıftan geliyor.
CreateMap<Source,Target> parametreleri ile çalışır. Bu şu demek; kod içerisinde source ile 
belirtilen obje tipi target ile belirtilen obje tipine dönüştürülebilir.
CreateMap<CreateBookModel, Book>();
Objeyi olduğu gibi çevirmek istiyorsak yani her tipteki obje field ları birbiri ile aynı 
olduğu durumda yukarıdaki tanımlama yeterlidir.

GetBookDetail e bakacak olursak orda nasil bir donusturme vardi ona bir bakalim
biz
  BookDetailViewModel vm=new BookDetailViewModel();
           book datasindaki verileri BookDetailViewModel imize set ederek burdan
           datalarini set ettigmiz viewmodeli return edecegiz...
           vm.Title=book.Title;
           vm.Genre=((GenreEnum)book.GenreId).ToString();
           vm.PageCount=book.PageCount;
           vm.PublishDate=book.PublishDate.Date.ToString("dd/MM/yyyy");

           Burda kaynak, Book, hedef ise BookDetailViewModel dir yani, id uzerinden
           gelen id nin hangi book a ait oldgunu bulduktan sonra kullaniciya direk book donmek
           istemedgimiz icin, buldugmuz book objesinin datlaarini BookDetailViewModel
           objesine akatriyoruz...
            public MappingProfile(){
                CreateMap<CreateBookModel,Book>();
                CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
              
                GetBookDetail deki map islemi direk birebir bir map degil yani
                Book icersidne GenreId int iken, BookDetailViewModel icinde string
                ayni sekilde tarih te oyle Book icinde Datetime, ama BookDetailViewModel
                icinde bu, string seklindedir dolayisi ile burda config,bu sekidle farkli sekilde olan satirlari
                nasil degistirecegini soylememiz gerekiyor
                VE bu donusumu biz aslinda burda yapacagiz direk
                src-source,sourcemiz Book, dest, destination ki o da BookDetailViewModel dir...
            }
             book datasindaki verileri BookDetailViewModel imize set ederek burdan
           datalarini set ettigmiz viewmodeli return edecegiz...
           BookDetailViewModel vm=new BookDetailViewModel();
           vm.Title=book.Title;
           vm.Genre=((GenreEnum)book.GenreId).ToString();
           vm.PageCount=book.PageCount;
           vm.PublishDate=book.PublishDate.Date.ToString("dd/MM/yyyy");
            BookDetailViewModel vm=_mapper.Map<BookDetailViewModel>(book);
           return vm;

            GetBookDetailQuery icerisiinde...
             List<BooksViewModel> vm=_mapper.Map<List<BooksViewModel>>(bookList);

             MappingProfile constructor icinde
              public MappingProfile(){
                CreateMap<CreateBookModel,Book>();
                CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
                CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
            }
                
*/