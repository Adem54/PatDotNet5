
namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateCommand
{
    public class CreateBookCommandTests 
    {

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