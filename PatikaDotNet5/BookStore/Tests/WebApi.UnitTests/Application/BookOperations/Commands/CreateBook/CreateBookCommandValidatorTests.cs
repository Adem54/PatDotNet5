
using FluentAssertions;
using WebApi.Application.BookOperatins.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.CreateBook;
using static WebApi.Application.BookOperatins.Commands.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{

    public class CreateBookCommandValidatorTests
    {

        //Validator context ve mapper kullanmiyor dolayisi ile onlara ihityacim olmayacak burda
        //Sadece validasyon yapacak burda
        // [Fact]//Fact bir kez, bir kosulda calisan test demektir
        //Eger ben bir kez yazdgimiz test methodunu birden fazla veri icin, birden fazla veri kadar calismasini
        //istiyorsam o nokta da [Theory] denilen bir yapi var, bu yapi ile birlikte, inline data verebiliyorum
        //O da bana bu test sinifinin birden cok kez, bu test icin calismasini sagliyor
        //Ben gidi bu test sinini birden fazla kez yazip sadece verisini degistirip yazmak istemiyorum
        //Yani gidip ayni test methjodunu 3-4 kez yazmak yerine 1 kez yazarak yapmak istiyorum [Theory] kullanarak
        //Once bir Fact[] ile yapalim sonra [Theory] ile refactor edelim

        //Invalid input verilirse validator sinifi geriye hata donsun
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            //Controller isinde CreateBookCommandValidator u nasil calistiriyorduk hatirlayalim
            // CreateBookCommandValidator validator=new CreateBookCommandValidator();
            // validator.ValidateAndThrow(command);
            //ARRANGE
            //Ilk once model i setlemem lazm
            //CreateBookCommand in constructorina context ve mapper yerine null yollayabiliriz cunku
            //onlarla isimiz  yok burda....sadee Model i setlememe ve gelen input degerlerini validat etme
            //islemini test edecegiz...
            CreateBookCommand command = new CreateBookCommand(null, null);
            /*
            RuleFor(command=>command.Model.GenreId).NotEqual(0).GreaterThan(0);
                    RuleFor(command=>command.Model.AuthorId).NotEqual(0).GreaterThan(0);
                    RuleFor(command=>command.Model.PageCount).GreaterThan(0);
                    RuleFor(command=>command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
                    RuleFor(command=>command.Model.Title).NotEmpty().MinimumLength(4);//En az 4 karakter olsun
            */
            //Burasi bizim test inputlarimiz
            //Bilerek hepsiini de hata alacak sekilde gonderip hata verecek mi onu test edecegiz zaten
            command.Model = new CreateBookModel()
            {
                Title = "",
                PageCount = 0,
                PublishDate = DateTime.Now.Date,
                GenreId = 0,
                AuthorId = 0
            };

            //ACT
            //Simdide Validator in instancesini olusturalim
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            //Daha sonra validator.ValidateAndThrow(command) u cagirmayacagiz da geriye
            //error response ve result u donmesini saglayacagiz
            var result = validator.Validate(command);
            //result altinda Error diye bir dizi var ve bu dizi altinda da
            //hata mesajlari var

            //ASSERT
            result.Errors.Count.Should().BeGreaterThan(0);//5 tane hata bekliyoruz yani 0 dan buyuk olmali
                                                          //Simdi biz 1 tane condition a gore ancak test  yazabiliyoruz [Fact] ile ama bizim CreateBookCommandValidator 
                                                          //icin daha en az 4-5 kontrol yapmam gerekiyor Title girildi digerleri girilmedi ise vs gibi gidip her biri 
                                                          //icin [Fact] attribute lu method yazmak cok maliyetlidir ondan dolyi biz [Theory] yi kullanarak
                                                          //bir test methodunda birden fazla kosul test edebilmeyi saglariz
        }

        [Theory]
        //Inline data vermek istiyoruz,test calisirken, parametrede setlenmesi gereken degerleri
        //inline olarak veriyoruz
        [InlineData("Lord Of The Rings", 0, 0, 0)]//Bu degerleri parametre deki degiskenlere set edilecek test yaparken
        [InlineData("Lord Of The Rings", 0, 1, 1)]
        [InlineData("Lord Of The Rings", 100, 0, 1)]
        [InlineData("Lord Of The Rings", 100, 1, 0)]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 100, 1, 2)]
        [InlineData("", 100, 0, 2)]
        [InlineData("", 100, 1, 0)]
        [InlineData("", 0, 1, 2)]
        [InlineData("Lor", 100, 1, 1)]//3 karaketer olayinin patlamasini da test ediyoruz burda
        [InlineData("Lor", 0, 0, 0)]
        [InlineData(" ", 100, 1, 2)]
        //Birde hersey valid verirsek, o zaman bu degerlerle test yaparken test patlayacak
      //  [InlineData("Lord Of The Rings", 100, 1, 2)]//Burda hic hata olmayacak dolayisi ile
        //result.Errors.Count.Should().BeGreaterThan(0); buna uymuyor..ve testi patlatacak...
        //Aslinda stres testi yapiyoruz yani her turlu hata cikabilecek varyasyonu deniyoruz,zorluyoruz
        //Bu testlerde ozellikle sinirlari denemek gerekiyor...
        //Burayi arttirabiliriz
        //Burda her bir inline data bir test case ine denk geliyor
        //Burda 1 tane test methodu ile 12 tane test yapmis oluyoruz....
        //Bu ayni bir test yaparken, farkli degerler vererek test etmemiz gerekiyor ise boyle durumlarda
        //kullanacgiz...

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors2(string title, int pageCount, int genreId, int authorId)
        {

            CreateBookCommand command = new CreateBookCommand(null, null);
           //Artik datalari statik degil de gelen veriye bagli olarak setlenmesini istiyoruz
            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                //DateTime dan patlamamasi icin-Bugunun tarihinden 1 yil oncesinin tarihini
                //PublishData e setle demis oluyoruz
                //DateTime ozelinde istisna bir durum var cunku DateTime.Now demek dependency demektir
                //Biz DateTime.Now ile datayi inline olarak veremeyiz cunku her bir test sirasinda
                //data nin degismesi demektir bu, verinin inputunun degismesi istenmeyen bir durumdur
                //Bundan dolayi Theory altindaki inline verilerde DateTime kullanamiyoruz
                //Bundan dolayi DateTime icin ayri bir case yazacacagiz...
                GenreId = genreId,
                AuthorId = authorId
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
              result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
             CreateBookCommand command = new CreateBookCommand(null, null);

             //Ben burda sadece tarih invalid verilirse hata alip almayacagimiz test
             //ediyorum bunu gorebilmek icin PublishDate disindaki degerler
             //valid olmalidir
             //Her bir test icinde sadece 1 case i test ediyor olmamiz gerekir yoksa
             //guvenilmez bir test olur o zamaan ondan dolayi da sadece publishDate i
             //test edecek 1 tane case yazacagiz ve testin de calismasini gormek icin
             //sadece date i invalid veririrz
               command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,//Bu sadedce gune bagli olarak kontrol yapiyor saat saniye 00 veriyor
                GenreId = 1,
                AuthorId = 1
            };

             CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);//sadece 1 tane hata versin diyoruz
        }


//Her kosunun dogru calistigi case i de yazalim onu da test edelim...
//Sadece hatalilari degil basarili case leri de test etmek gerekiyor
         [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
             CreateBookCommand command = new CreateBookCommand(null, null);
            //Tum degerleri valid vererek hata almadan calistigimizi test edecegiz burda
               command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(2),//Bu sadedce gune bagli olarak kontrol yapiyor saat saniye 00 veriyor
                GenreId = 1,
                AuthorId = 1
            };

             CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Equals(0);//Burda da hic hata vermemesini test et diyoruz...
        }
    }
}