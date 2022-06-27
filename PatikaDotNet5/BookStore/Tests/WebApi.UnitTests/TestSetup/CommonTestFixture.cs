
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common.MappingProfile;
using WebApi.DbOperations;

namespace WebApi.UnitTests.TestSetup {
    public class CommonTestFixture {
        //Benim bir DbContext e 1 de Mapper e ihtiyacim var
        public BookStoreDbContext Context {get; set;}
        public IMapper Mapper {get; set;}
        public  CommonTestFixture()
        {
            //Ilk once DbContextin optionslairni olusturalim
            var options=new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName:"BookStoreTestDB").Options;
            //optionslari aldik
            Context=new BookStoreDbContext(options);
            //Context olusturmus olduk
            //Burda BookStoreDbContext in options ini degistirip olusturduk, yani optionslari
            //Test projesi icinden verdik
            //Context in olusturuldugundan emin olamk icin ise soyle birsey yapabiliriz
            Context.Database.EnsureCreated();
            //Intial datalar ekleyecegiz simdide WebApi projemizde DataGenerator icinde yapmistik
            //Bizim Books,Genres,Authors listesine ihtiyacimiz var, initial olarak yapalim diyoruz yani bos bir database olmasin diye
            //TestSetup altinda Books.cs olustruruzu bu islem icin
            //Datalarimizi BookStoreDbContext tipiinden olusturulmus instanceler uzerinden
            //ekleyebilmek icin Books,Authors,Genres icin extension method olusturduk
            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            //Context ile isimiz bitti,simdi Mapper i configure edelim
            //Bizim WebApi projesindeki MappingProfile file i Profile olarak buraya gosteriyor olmamiz gerekiyor
            //ki uygulama icerisinde mapping ile ilgili, Book Model i ,BookViewMode e cevirmek istedigmde
            //configleri yine gidip ordan okuyor olmali,ben bu nesneyi burda olusturdum ama
            //configleri git  ordan al demis oluyoruz...ki uygulama icerisinde ordkai config de birsey 
            //degistiginde gelip benim test sinifim patlamasin diye,configlerimizde bir problem olmasin diye
            //WebApi uygulamasinda kullanilan MappingProfile dan configurasyonlari alarak kendi mapper imizi
            //olusturduk
            Mapper=new MapperConfiguration(cfg=>{cfg.AddProfile<MappingProfile>();}).CreateMapper();
        }
    }
}

/*
Commandlarimiz icinde bu 2 tane property yi injekt ediyor olmmamiz gerekiyor

*/