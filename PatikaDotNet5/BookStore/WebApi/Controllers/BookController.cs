
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperatins.CreateBook;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperatins.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBookDetail.GetBookDetailQuery;

using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

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

        private readonly BookStoreDbContext _context;
        //Uygulama icerisinde degistirilemesin diye readonly yapariz....bu onemli...bunu iyi bilelim..

        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
           
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
    GetBooksQuery query=new GetBooksQuery(_context,_mapper);
    var result=query.Handle();
    return Ok(result);
    /*
    Biz artik burda hem 200 bilgisini don hemde, objeyi don diyecegiz..
    Tabi bunu yapabilemiz icin IActionResult dondurursek, tipten bagimsiz olarak
    http Ok bilgisi ile birlikte datayi da donmek istersek iste bu sekilde ActionResult doneriz..
    */
}

[HttpGet("{id}")]//https://localhost:5001/Books/1
public IActionResult GetBookById(int id){
    GetBookDetailQuery query=new GetBookDetailQuery(_context,_mapper);
    BookDetailViewModel result;
    try
    {
        query.BookId=id;
        GetBookDetailQueryValidator validator=new GetBookDetailQueryValidator();
        validator.ValidateAndThrow(query);
        result=query.Handle();    
    
    }
    catch (Exception ex)
    {
        
        return BadRequest(ex.Message);
    }
    return Ok(result);
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
Simdi gidip bu response action imizi da refactor edecdgiz...BookOpeartions klasoru altinda
*/
/*
Biz artik disardan kullanicidan CreateBookModel tipinde bir data alacagim , bu data icinde Id yok id dahil degil
cunku kullanicidan bize id gelmeyecek add islemlerinde.....

*/
   public IActionResult AddBook([FromBody]CreateBookModel newBook){
    CreateBookCommand command=new CreateBookCommand(_context,_mapper);
    try
    {
        command.Model=newBook;
        
        //Validasyonu burda yapacagiz..
       CreateBookCommandValidator validator=new CreateBookCommandValidator();
        //ValidationResult result=validator.Validate(command);
        validator.ValidateAndThrow(command);//usingFluentValidation dan geliir...
        //FluentValidationResult tan geliyor ValidationResult-validator.Validate deki 
        //Validate ise AbstractValidator den geliyor...
        //Su an result bir objedir ve icerisinde, bizim validasyonu yapabilmemiz icin 
        //olusturulums kontrol propertyleri var isValid gibi mesela...
        //Tum kurallardan gecti ise isValid true doner gecmedi ise isValid false doner..
        //IsValid ile beraber kurallardan hangilerinden gecemedi isem onu da bize mesaj
        //ile ifade ediyor, result.Errors un altinda bir failure diye obje barindiriyor
        
        // if(!result.IsValid){
        //     foreach (var item in result.Errors)
        //     {
        //         Console.WriteLine("Property "+ item.PropertyName+ "Error Message: "+ item.ErrorMessage);
                //icinde bulundugum hata aldgimi objenin hangi field i hata aliyor bana
                //bunu soyler-item.PropertyName
                //Ayrica hangi hata mesajini firlatildigni soyler ki, biz bir bir field a bir den fazla
                //kural yaziyoruz ve hangi kuralin cignendini de ancak mesaj la anlayabiliriz
        //     }
        // }else   
        command.Handle();
           //ONEMLI.....
        //CreateBookCommand class icindeki CrateBookModel tipindeki Model prperties ine kullanicidan
        //gelen datayi atama  yapiyoruz ki, biz bu gelen datayi, CrateBookCommand class inda Handle icinde
        //Book entitisinin alanlarini gelen data ile doldurelim...
        //Biz CreateBookCommand class i icinden kullanici ayni kitap isminde bir kitap godnerirse
        //kullanicya exception firlattik bu exception i biz BookController dan da kullaniciya 
        //dogru birsekilde donebilmem gerekiyor,normlde throw hata firlatirsa kodu kiracaktir
        //Iste bu hata mesajini benim burda anlamli bir sekilde yakalayabilmem icin try-catch ile
        //bu islemi ele alacagim
    }
    catch (Exception ex)//ex aslinda bizim mesajimiz
    {
        //Ayni kitap bir daha eklenmeye calisilirsa o zaman hata firlatacakki kullanici da anlayacak
        //kitabin neden eklenemedgini
        return BadRequest(ex.Message);
        //Bizim,CreateBookCommand class icersindeki firlattgimiz exception in mesaj ini donder diyoruz.. 
    }
    

   
    return Ok();
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
public IActionResult UpdateBook(int id,[FromBody]UpdateBookModel updatedBook){
    try
    {
        UpdateBookCommand command=new UpdateBookCommand(_context);
         command.BookId=id;
         command.Model=updatedBook;
         UpdateBookCommandValidator validator=new UpdateBookCommandValidator();
         validator.ValidateAndThrow(command);
         command.Handle();
    }
    catch (Exception ex)
    {
        
        return BadRequest(ex.Message);
    }

     return Ok();
}

[HttpDelete("{id}")]

    public IActionResult DeleteBook(int id){
            try
            {
                DeleteBookCommand command=new DeleteBookCommand(_context);
                command.BookId=id; 
                //Burda validation i BookId yi setledikten sonra ve de Handle methodundan
                //once yapmamiz gerekir...BookId yi setlemeden once verirsek, hata verir 
                //cunku int bir alan ve default olarak 0 verecek
                DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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