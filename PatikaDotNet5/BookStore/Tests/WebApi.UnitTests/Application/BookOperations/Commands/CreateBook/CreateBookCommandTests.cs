
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperatins.Commands.CreateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using static WebApi.Application.BookOperatins.Commands.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        //Bizim bu test class imiza CommonTextFixture configure ayarlarini ayapan
        //class imizi tanitiyor olmamiz gerekiyor, IClassFixture<> inherit edeceksin diyoruz
        //IClassFixture<> xunitten gelir ve generic olarak bizim olustrudgmuz
        //configur islemleri icin, olusturuduguz class i veririz CommonTestFixture
        //CommontTestFixture u biz burda kullanarak onun bize sagladigi DbContext ve
        //Mapper a erisebilir duruma geliyoruz,IClassFixture bize bunu sagliyor
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            //Su anda CommonTestFixture u biz burda instance olusacak ve constructor i 
            //calisacak ve bu sekilde bizim configurasyonlarimiz gerceklesmis olacak
            //VE biz CreateCommandTests constructor i icerisinden yapilan 
            //configlere erisebilir durumdayiz artik
            //VE de diyoruz ki sen testFixture dan gelen contexti ve mapper i kullan diyoruz
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;

        }
        /*
        Neyi test edecegiz simdi ona bir bakalim, CreateBookCommand da yapilanlara
         1- public void Handle(){
                var book=_dbContext.Books.SingleOrDefault(book=>book.Title==Model.Title);
                    if(book is not null)
                        throw new InvalidCastException("Kitap zaten mevcut!");
               Burda ayni kitapta kitap var ise, bir exception firlatiyor
               Buranin testini olsturabiliriz...dogru calisp calismadingi 
                       
        */
        //ISIMLENDIRME ILE ILGILI BESTPRACTISE
        //Test ler genel olarak void tipindedir birsey donmezler geriye
        //Var olan bir kitabin title i verildiginde geriye invalid operatoin exception donsun istiyoruz
        //Zaten var olan bir kitap ismi verildiginde InvalidOperationException geri dondurmeli...
        [Fact]//Bu bir test metodu oldugunu soylemis oluyoruz.....bu onemlidir...
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //1-Arrange-(Hazirlik)
            //2-Act-(Calistirma)
            //3-Assert-(Dogrulama,saglama)

            //ARRANGE
            //Zaten icerde var olan bir kitabin title ini gondermem gerekiyor
            //Sadece bu test kapsaminda bir tane kitap olusturacagim
            //Cunku biz sadece bu test kapsaminda olacak,calisacak uniq veriler olusturmaya calisiyoruz
            //Sadece bu test calisirken bir veri olusturulsun ve bu veri test bittgiinde de bitsin istiyoruz
            var book = new Book() { Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new System.DateTime(1990, 01, 10), GenreId = 1, AuthorId = 2 };
            //Bunu olusturduk ama database e kaydetmedik...
            _context.Books.Add(book);
            _context.SaveChanges();
            //Simdi de command imizin nesnesini olustrururuz
            //CreateBookCommand in constructor ina artik benim test im icin olusturdugm _context i
            //ve _mapper i veriyor
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            // public CreateBookModel Model {get; set;} boyle bir model i var bu modeli setlyecegiz
            //Bunun da bu test icin sadece bizim title a ihtiyacimiz var,cunku bu yazdgimiz test
            //bize kullanicidan gelen model deki title a bakip onu kontrol ediyor ve hata firlatiyor
            //Yani gidip diger degerlerini set etmemize gerek yok, sadece Title i alsak yeter
            //Title olarak da hata vermesi icin, bilerek az once db ye yazdgimiz book.Title veririz
            //Hata verip vermedigini anlamak icin
            command.Model = new CreateBookModel() { Title = book.Title };

            //ACT & ASSERTION
            //Burda fluent assertion kullanilarak act ile assert birlikte yapilir
            //Invoking icinde caslismasi gereke methodu verilir
            //Sonrasinda da methodun sonucunu kontrol ediyoruz...
            //command.Handle su anda benim Test projem den gonderilen, context in icine
            //bakiyor yani WebApi projesindeki context e degil dikkat edelim
            FluentActions
            .Invoking(() => command.Handle())//bu method calistirildiginda     //InvalidOperationException bu hatayi firlatmali                  
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Kitap zaten mevcut!");
            //Ayrica mesaji da bizim CreateCommandBook da verdigmiz "Kitap zaten mevcut!" hata mesaji
            //olmali diyoruz...
            //Dikkat edelim, hangi tur bir hata mesaji doneceginden, tutalim da hata mesajina
            //kadar test edebiliyoruz...
            //Bunu soyle dusunelim, burasi cok onemli bir logic olsa idi ve bu hata
            //mesaji baska bir yerde kullaniliyor olsa idi, ve ben test methodu icinde
            //dogru hata mesajini test ettigimi bilse idim o zaman direk uygulama icinde
            //yanlis hata mesaji dondugumuzu bulurduk
          
        }

         [Fact]//Burda yeni bir book objesi database e eklenecek mi onu  test ediyoruz
        public void WhenValidInputIsGiven_Book_ShouldBeCreated()
        {
           
            //arrange
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel() { Title = "Hobbit",PageCount=134,PublishDate=DateTime.Now.Date.AddYears(-10),GenreId=1,AuthorId=2 };
            command.Model=model;

            //Act
            FluentActions
            .Invoking(() => command.Handle()).Invoke();
            //Burda should ile kontrol etmeyecegiz, should ile kontrol etmez ve de invoke da demez isek o zaman
            //method u calistirmiyor, onun icin Should demiyorsak Invoke diyerek mehtodu calistirmamiz gerekiyor 
            //Should otomatik olarak bir geri donus degerini kontrol ettigi icin otomatik olarak calismasni sagliyor   
            //Eger donusu kontrol etmeyip sadece calistiracak isek de o zaman Invoke methodunu calistiririz
            //Eger kitap title i database de olmayan bir title gelirse, ve de validation rules lara da takilmayacak sekilde
            //gelecek sekilde gelirse bursa command.Handle() in bir hata dondurmesini beklemiyoruz      

            //Assert
            //Burda ne olmasini bekliyoruz, eger hata vermedi ise o zaman bu kitap bilgisi database e eklenmis olmali
            //O zaman bu kitap bilgisi database de var mi yok mu onu kontrol etmem gerekiyor...
            //Database e bir selece atarak cek edelim...
            var book=_context.Books.SingleOrDefault(b=>b.Title==model.Title);
            //burda zaten title a ait bilgi bulamazsa o zamn book null gelir ve NotBeNull() da patlar zaten
            //Title i kontrol etmeye gerek yok...o yuzden zaten SingleOrDefault ile kontrol etis oluyoruz
            //book u olusturuldugunu anlamak icin herseyini kontrol edecegiz....
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
          //  book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
            
          
        }

    }
}

/*
Ben bu class yapimizi test edebilmek icin, bizim CreateBookCommand in isntancesinie ihtiyacimiz var
Peki bir class instancesi olustururken ne oluyor onun constructor lari da cagriliyor
CreateBookCommand.cs nin constructor i bizden IBookStoreDbContext _context i ve IMapper _mapper 
i bekliyor 
CreateBookCommand command=new CreateBookCommand(_context,_mapper);
_context BookStoreDbContext somut sinifindan cagriliyor ama biz kesinlikle bagimli olunan
somut siniflari direk testimizde kullanmayacaktik onlarin fake lerini olusturup onlari testte kullanacagiz
Biz birim testlerde inmemory database i kullanabilecegimizden bahsetmistik
Bizim CreateBookCommand i test edebilmek icin sanki database e yaziyor gibi yapmamiz gerekiyor....
Dolayisi ile test icerisinde de inmemory database kullanmayi planliyoruz...
Bizim testprojesi kapsaminda IBookStoreDbContext icin controller da Startup.cs nin calismasi ile
kullandigmiz somut karsiligini nasil ki Configurasyonda onu gecmistik
burda cagiririken de bizim burda configure etttimiz DbContext i  ve ayni sekilde 1 tane de Mapper
sinifini oraya gecmemz gerekiyor, yani bu ikisini testprojesi kapsaminda benim moq yardimi ile
configurasyonu burdan vermem gerekiyor, yani test projesinde artik benim olusturdugjm fake
somut siniflardan almali instanceyi.......
Test projesi hicbirsekilde uygulama icerisindeki CreateBookCommand icindeki hicbir logic ten
hicbir dis etkenden etkilenmiyor olmasi gerekir,bu tarz injection ile verilen herseyin testprojesi
icerisinden gonderiliyor olmasi gerekir ki testimiz guvenilir olsun ve her seferinde ayni cevabi 
versin...Dolayisi ile bir config yapmamiz gerekir
WebApi.UnitTests altinda Application ile ayni duzeydeki dizinde 1 tane TestSetup isminde bir 
klasor acariz onun altina da CommonTextFixture.cs olustururuz bu bize DbContext ve Mapper i
verecek olan siniftir
Biz WebApi projesini, WebApi.UnitTests projesine referans olarak ekldigmiz icin WebApi projesi
icindeki class lari artik WebApi.UnitTests projesijnde kullanabiliyorum....
WebApi.UnitTests projemize 1 nuget pacakage yuklyecegiz Enittyframeworkcore inmemory yi ekleyecegiz
Cunku biz DbContext i n fake ini kendi Test projemiz icinde olusturup IBookStoreDbContex te instancenin
Test icinde olusturdgumuz somut siniftan gelmesini saglayacagiz kullanacagiz...
Database in bir mockunu-taklitini yapip onu kullanacagim icin entityframework un inmemory-database ini
 kullancagiz
 Test edilecek projde MsSql,Oracle vs kullanilabilir ama Test projesinde cogu zaman inmemery dataase
 ismizi gorecektir


*/