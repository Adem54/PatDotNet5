

using System;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetailQuery{

    public class GetBookDetailQuery{

//bu mantigi iyi anlayalim, kullanicidan gelecek input degeri yani parametreye gelecek olan deger 
//ki ilk  kullanicinin requestini  karsiladigmiz yer Kaynak,resourcemiz olan response actionlarimizdir
//Orda kullanicidan gelecek olan id, ile  burdaki properety miz olan BookId set edilir ki, biz sonrasinda
//Handle icinde BookId yi kullanabilelim... BESTPRACTISE...COOOK ONEMLI..
        public int BookId {get; set;}
        private readonly BookStoreDbContext _dbContext;

        public GetBookDetailQuery(BookStoreDbContext dbContext)
        {
            _dbContext=dbContext;
        }

   //Mantiga cok dikkat edelim, disardan kullanicidan gelecek olan input, parametreye alacagimiz degeri biz
   //her bir CRUD operasyonunu spesifiklestirdgimiz bu GetBookDetailQuery,CreatBookCommand gibi class lara property olarak
   //verdgimiz degerler ile, disardan gelecek olan inputu alacagiz.... ve burdaki Handle fonksiyonu calistigi zaman,
   //zaten Handle icinde kullanmamiz gereken, input a ait datalari biz dogrdann icinde calistiigmi class in property
   //sinden alaagiz, cunku controllerda biz, disardan data geldginnde parametre de hangi tur vs alacaksak zaten onu da 
   //bu her bir CRUD operasyounu spesifiklestirdigmiz class icinde alacagiz ve de controller da biz, bu sepsifiklestirdigmiz 
   //REsponse actionlarinin , controller responnse actionlarinin icinde de, spesifiklestiridgimiz methodlar icin verdgimiz, 
   //propertiesleri set ederek, artik bunlarin, kendi class lari icinde dogurdan o properties icinde kkullanilabilmesini sa
   //sagliyoruz...Ya bu mantik bizim, bir Atm uygulamasi veya bir kahve makinesi mantigina da cok benziyor, her bir rakam
   //bir isleme karsilik geliyordu ve biz, 1 tane tum islemlerin yapildigi, class kullanirdik sonra 1 tane data nin geldigi
   //class oluyordu ve sonra her bir Crud operasyonu iicn ayri ayri class yazip her bir class icinde ayni isimde Run gibi bir method
   //isminin altina disardan enjekte ettgimiz, tum operasyonlari barindiran, class i her bir spesifik Crud operasyonu altinda hangi methoda denk geliyorsa onu invoke ederek, ayni isimde mehtodlarin altinda her bir CRUD operasyonunda kendi islmeni invok e ettirrererk bunlari ayrica da ayni interface altinda toplayarak
   //hepsini bir liste de toplama firsatini elde ederdi, ve   bir de her bir CRUD class ina property eklerdik
   //O Crud operasyonunun hangi numaralai isleme karsiklik geldigi yani kullanicidan disardan gelecek olan bilgi,,,,hangi islemi 
   //yapmak istiorsunuz diye sorarz onlarda cevap verir, yani inputu verir yani parametreyi verir biz de o parametreyi alip kullanirdik
   //O mantik ile ayni mantiktir...Biz de burda disardan gelecek olan data yi direk entity ye mnuhatap etmiyoruz, ve onun icin
   //de bir data model olusturyuyoruz, Ayrica da disardan gelecek olan datayi biz, Crud operasyonlarini ayri ayri
   // yazdigmiz class larda property olarak yaziyoruz.
        public BookDetailViewModel Handle(){
            var book=_dbContext.Books.Where(book=>book.Id==BookId).SingleOrDefault();
            //BESTPRACTISE..HERZAMAN KULLANICININ GONDERDIGI INPUT UZERINDEN DATAYA ERISILMEZ ISE...
            //Ilk once boyle bir id de book yok ise diye bir kontrol yapmaliyiz her zaman
            //unutmayalim, adam id gonderdi ama oyle bir book yok bende nasl bir karsilik
            //donmeliyim kullaniciya......
            if(book is null)
            throw new InvalidOperationException("Kitap bulunamadi");
           //Dikkat edelim once burda kullanicidan gelecek id uzerinden veritabanindan
           //book datamizi aliriz ardindan ise bu book u, viewmodel e map lemem lazm
           //onun icinde view model in once bir nesnesini olusturmamz gerekiyor
           BookDetailViewModel vm=new BookDetailViewModel();
           //book datasindaki verileri BookDetailViewModel imize set ederek burdan
           //datalarini set ettigmiz viewmodeli return edecegiz...
           vm.Title=book.Title;
           vm.Genre=((GenreEnum)book.GenreId).ToString();
           vm.PageCount=book.PageCount;
           vm.PublishDate=book.PublishDate.Date.ToString("dd/MM/yyyy");
           return vm;
        }

        //Her bir UI icin, her bir view icin kendi viewmodel ini kullanacagiz, ayri bir view model kullanmaliyiz
        //demisitik

        public class BookDetailViewModel {
            public string Title {get; set;}
            public string Genre {get; set;}
            public int PageCount {get; set;}
            public string PublishDate {get; set;}
        }
    }
}