
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.UnitTests.TestSetup {
    public class CommonTestFixture {
        //Benim bir DbContext e 1 de Mapper e ihtiyacim var
        public BookStoreDbContext Context {get; set;}
        public IMapper Mapper {get; set;}
        public CommonTestFixture()
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
        }
    }
}

/*
Commandlarimiz icinde bu 2 tane property yi injekt ediyor olmmamiz gerekiyor

*/